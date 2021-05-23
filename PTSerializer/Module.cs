using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProTrackerTools
{
    public class Module
    {
        public string Name { get; set; }
        public Sample[] Samples { get; set; }
        public const int MaxSamples = 31;
        public int SongLength { get; set; }
        public int[] SongPositions { get; set; }
        public List<Pattern> Patterns { get; set; }
        public byte Unknown { get; set; }
        public Module()
        {
            Samples = new Sample[MaxSamples];
            SongPositions = new int[128];
            for (var i = 0; i < MaxSamples; i++)
            {
                Samples[i] = new Sample();
            }
            Patterns = new List<Pattern>();
        }

        public Pattern CreateNewPattern()
        {
            var pattern = new Pattern();

            pattern.Channels = new Channel[4];

            for (var c = 0; c < 4; c++)
            {
                var chan = new Channel();
                chan.PatternItems = new PatternItem[64];
                for (var l = 0; l < 64; l++)
                {
                    chan.PatternItems[l] = new PatternItem();
                }
                pattern.Channels[c] = chan;
            }

            return pattern;
        }

        /// <summary>
        /// Removes unwanted data after the loop end point
        /// </summary>
        public void OptimseLoopedSamples()
        {
            foreach (var sample in this.Samples)
            {
                if (sample.RepeatLength > 2)
                {
                    var newLength = sample.RepeatStart + sample.RepeatLength;
                    if (newLength < sample.Length)
                    {
                        sample.Data = sample.Data.Take(newLength).ToArray();
                        sample.Length = newLength;
                    }
                }
            }
        }

        /// <summary>
        /// Made for Atari users, pads samples based on the size set
        /// </summary>
        /// <param name="SampleSize"></param>
        public void PadSamples(int SampleSize)
        {
            for (var sIndex = 0; sIndex < Samples.Count(); sIndex++)
            {
                var s = Samples[sIndex];

                if (s.Length > 2)
                {
                    var sample = new Sample()
                    {
                        Name = s.Name,
                        Volume = s.Volume,
                        FineTune = s.FineTune,
                        RepeatStart = s.RepeatStart,
                        RepeatLength = s.RepeatLength
                    };

                    var newLength = ((s.Length / SampleSize) * SampleSize) + SampleSize;
                    sample.Length = newLength;

                    var pos = 0;
                    var repeatEnd = sample.RepeatStart + sample.RepeatLength;
                    sample.Data = new byte[newLength];

                    for (var i = 0; i < newLength; i++)
                    {
                        if (pos < s.Data.Length)
                        {
                            sample.Data[i] = s.Data[pos];       // copy byte
                        }
                        else
                        {
                            sample.Data[i] = 0x00;
                        }

                        pos++;                              // move to next byte

                        if (sample.RepeatLength > 2 && pos == repeatEnd)                 // loop bounds check
                        {
                            pos = sample.RepeatStart;
                        }
                    }

                    Samples[sIndex] = sample;
                }
            }
        }

        /// <summary>
        /// Removes any samples which have not been used in the pattern data
        /// </summary>
        public void RemoveUnusedSamples()
        {
            // find all used samples
            var used = new bool[32];
            foreach (var pattern in Patterns)
            {
                foreach (var chan in pattern.Channels)
                {
                    foreach (var item in chan.PatternItems)
                    {
                        used[item.SampleNumber] = true;
                    }
                }
            }

            // clear any unsed samples
            for (var i = 1; i < used.Length; i++)
            {
                if (!used[i])
                {
                    Samples[i - 1].Data = new byte[2];
                    Samples[i - 1].Length = 0;
                    Samples[i - 1].RepeatStart = 0;
                    Samples[i - 1].RepeatLength = 2;
                    Samples[i - 1].FineTune = 0;
                }
            }
        }

        /// <summary>
        /// Clears the first two bytes of each single shot sample for people who don't use a proper tracker
        /// </summary>
        public void ZeroLeadingSamples()
        {
            foreach (var sample in Samples)
            {
                if (sample.Length > 2 && sample.RepeatLength == 2 && sample.RepeatStart == 0)
                {
                    sample.Data[0] = 0;
                    sample.Data[1] = 0;
                }
            }
        }

        /// <summary>
        /// Clears patterns after song end point and removes and sanitizes the remaining patterns
        /// </summary>
        public void RemoveUnusedPatterns()
        {
            // zero out anything after song end
            for (var i = 0; i < 128; i++)
            {
                if (i >= SongLength)
                {
                    SongPositions[i] = 0;
                }
            }

            // based on song length, mark which patterns are actually used
            var used = new bool[100];
            for (var i = 0; i < SongLength; i++)
            {
                used[SongPositions[i]] = true;
            }

            // copy highest used pattern to lowest unused pattern
            for (var current = 0; current < used.Length; current++)
            {
                if (!used[current])
                {
                    var lastUsed = FindLast(true, used);
                    if (lastUsed > current)
                    {
                        Patterns[current] = Patterns[lastUsed];
                        RemapPattern(lastUsed, current);
                        used[current] = true;
                        used[lastUsed] = false;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            SanitizePatterns();
        }

        private void SanitizePatterns()
        {
            var newPatterns = new List<Pattern>();
            var maxUsedPattern = SongPositions.Max();
            for (var p = 0; p <= maxUsedPattern; p++)
            {
                newPatterns.Add(Patterns[p]);
            }
            Patterns = newPatterns;
        }

        /// <summary>
        /// Compares all patterns to remove and the remaps any duplicates
        /// </summary>
        public void RemoveDuplicatePatterns()
        {
            for (var a = 0; a < Patterns.Count; a++)
            {
                for (var b = a + 1; b < Patterns.Count; b++)
                {
                    var pata = Patterns[a];
                    var patb = Patterns[b];
                    if (PatternCompare(pata, patb))
                    {
                        RemapPattern(b, a);
                    }
                }
            }

            SanitizePatterns();
        }

        private bool PatternCompare(Pattern a, Pattern b)
        {
            for (var line = 0; line < 64; line++)
            {
                for (var chan = 0; chan < 4; chan++)
                {
                    var itema = a.Channels[chan].PatternItems[line];
                    var itemb = b.Channels[chan].PatternItems[line];

                    if (itema.SampleNumber != itemb.SampleNumber) return false;
                    if (itema.Period != itemb.Period) return false;
                    if (itema.Command != itemb.Command) return false;
                    if (itema.CommandValue != itemb.CommandValue) return false;
                }
            }
            return true;
        }

        private void RemapPattern(int source, int dest)
        {
            for (var p = 0; p < 128; p++)
            {
                if (SongPositions[p] == source)
                    SongPositions[p] = dest;
            }
        }

        private int FindLast(bool state, bool[] blist)
        {
            for (var i = blist.Length - 1; i >= 0; i--)
            {
                if (blist[i] == state) return i;
            }
            return -1;
        }

        /// <summary>
        /// Truncates a sample to the loop contents only, returns true if successful
        /// </summary>
        /// <param name="sampleId"></param>
        public bool TruncateToLoop(int sampleId)
        {
            var sample = Samples[sampleId];
            if (sample.RepeatLength > 2 && sample.RepeatStart > 2)
            {
                sample.Data = sample.Data.Skip(sample.RepeatStart - 2).ToArray().Take(sample.RepeatLength + 2).ToArray();
                sample.Length = sample.Data.Length;
                sample.RepeatStart = 2;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Expands all patterns which may contain E6 loops
        /// </summary>
        /// <returns></returns>
        public void ExpandPatternLoops()
        {
            int patCount = Patterns.Count();
            for (var patIndex = 0; patIndex < patCount; patIndex++)
            {
                var pat = Patterns[patIndex];

                if (ContainsPatterLoop(pat))
                {
                    // create expanded pattern
                    var expanded = ExpandPattern(patIndex);

                    // add new patterns to Mod and generate Ids
                    var patIdList = new List<int>();
                    foreach (var newPat in expanded)
                    {
                        Patterns.Add(newPat);
                        patIdList.Add(Patterns.IndexOf(newPat));
                    }

                    var sourcePatId = Patterns.IndexOf(pat);

                    // re-map patterns in the song
                    var songPos = new List<int>();
                    for (var i = 0; i < SongLength; i++)
                    {
                        var patId = SongPositions[i];

                        if (patId == sourcePatId)
                        {
                            songPos.AddRange(patIdList);
                        }
                        else
                        {
                            songPos.Add(patId);
                        }
                    }

                    if (songPos.Count > 128)
                    {
                        throw new Exception("Song position overflow");
                    }
                    else
                    {
                        int index = 0;
                        foreach (var i in songPos)
                        {
                            SongPositions[index] = i;
                            index++;
                        }
                        SongLength = songPos.Count;
                    }
                }
            }
        }

        private bool ContainsPatterLoop(Pattern pattern)
        {
            foreach (var chan in pattern.Channels)
            {
                bool loopFound = false;
                foreach (var item in chan.PatternItems)
                {
                    if (item.Command == CommandType.Extended && (item.CommandValue & 0xF0) == 0x60)
                    {

                        if ((item.CommandValue & 0x0F) == 0)
                        {
                            loopFound = true;
                        }

                        if ((item.CommandValue & 0x0F) > 0 && loopFound)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Creates multiple patterns where E6 loops are present
        /// </summary>
        /// <returns></returns>
        private List<Pattern> ExpandPattern(int patternId)
        {
            var source = Patterns[patternId];
            var newPatterns = new List<Pattern>();
            var dest = new Pattern();

            int newLineId = 0;
            int totalLines = 0;

            var loops = new PatternLoop[4];
            for (var i = 0; i < 4; i++)
            {
                loops[i] = new PatternLoop();
            }

            int lineId = 0;
            while (lineId < 64)
            {
                for (var chan = 0; chan < 4; chan++)
                {
                    // create new copy of pattern item
                    var item = DupePatternItem(source.Channels[chan].PatternItems[lineId]);
                    dest.Channels[chan].PatternItems[newLineId] = item;

                    if (item.Command == CommandType.Extended && (item.CommandValue & 0xF0) == 0x60)
                    {
                        if ((item.CommandValue & 0x0F) == 0)            // set loop start point
                        {
                            if (!loops[chan].Active)
                            {
                                loops[chan].LoopStart = lineId;
                                loops[chan].Done = false;
                            }
                        }
                        else if ((item.CommandValue & 0x0F) > 0)        // set loop end point and activate
                        {
                            if (!loops[chan].Active && !loops[chan].Done)
                            {
                                loops[chan].LoopEnd = lineId;
                                loops[chan].LoopCount = (item.CommandValue & 0x0F);
                                loops[chan].Active = true;
                            }
                        }

                        item.Command = CommandType.ArpNone;
                        item.CommandValue = 0;
                    }
                }

                int loopLineId = -2;
                // now line is done, check the loops
                foreach (var loop in loops)
                {
                    if (loop.Active && loop.LoopEnd == lineId)
                    {
                        loop.LoopCount--;
                        if (loop.LoopCount < 0)
                        {
                            loop.Active = false;
                            loop.Done = true;
                        }
                        else
                        {
                            loopLineId = loop.LoopStart - 1;
                        }
                    }
                }

                // resulted in loop start point, now set
                if (loopLineId >= -1)
                    lineId = loopLineId;

                newLineId++;
                totalLines++;
                lineId++;

                // current pattern full, add to list and create new
                if (newLineId > 63)
                {
                    newPatterns.Add(dest);
                    dest = new Pattern();
                    newLineId = 0;
                }
            }

            if (newLineId != 0)
            {
                // add break command to pattern end
                bool breakDone = false;
                foreach (var chan in dest.Channels)
                {
                    var lineBreak = chan.PatternItems[newLineId - 1];
                    if (lineBreak.Command == CommandType.ArpNone && lineBreak.CommandValue == 0x00)
                    {
                        lineBreak.Command = CommandType.PatternBreak;
                        breakDone = true;
                        break;
                    }
                }

                if (!breakDone)
                {
                    throw new Exception("Unable to find space for pattern break");
                }

                // clean up pattern tails
                foreach (var chan in dest.Channels)
                {
                    for (var i = 0; i < 64; i++)
                    {
                        if (chan.PatternItems[i] == null)
                        {
                            chan.PatternItems[i] = new PatternItem()
                            {
                                Command = CommandType.ArpNone,
                                CommandValue = 0x00,
                                Period = 0,
                                SampleNumber = 0
                            };
                        }
                    } 
                }

                newPatterns.Add(dest);
            }

            return newPatterns;
        }

        private PatternItem DupePatternItem(PatternItem source)
        {
            return new PatternItem()
            {
                SampleNumber = source.SampleNumber,
                Period = source.Period,
                Command = source.Command,
                CommandValue = source.CommandValue
            };
        }

        public void ClearSampleNames()
        {
            foreach (var sample in Samples)
            {
                sample.Name = Encoding.ASCII.GetString(new byte[22]);
            }
        }
    }

    class PatternLoop
    {
        public int LoopStart { get; set; }
        public int LoopEnd { get; set; }
        public int LoopCount { get; set; }
        public bool Active { get; set; }
        public bool Done { get; set; }
    }

    public class Sample
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public int FineTune { get; set; }
        public int Volume { get; set; }
        public int RepeatStart { get; set; }
        public int RepeatLength { get; set; }
        public byte[] Data { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Pattern
    {
        public Channel[] Channels { get; set; }

        public Pattern()
        {
            Channels = new Channel[4];
            Channels[0] = new Channel();
            Channels[1] = new Channel();
            Channels[2] = new Channel();
            Channels[3] = new Channel();
        }
    }
    
    public class Channel
    {
        public PatternItem[] PatternItems { get; set; }

        public Channel()
        {
            PatternItems = new PatternItem[64];
        }
    }

    public class PatternItem
    {
        public int SampleNumber { get; set; }
        public int Period { get; set; }
        public CommandType Command { get; set; }
        public byte CommandValue { get; set; }
    }

    public enum CommandType
    {
        ArpNone = 0x00,
        SlideUp = 0x01,
        SlideDown = 0X02,
        TonePortamento = 0x03,
        Vibrato = 0x04,
        TonePortamentoVolumeSlide = 0x05,
        VibratoVolumeSlide = 0x06,
        Tremolo = 0x07,
        UnusedSync = 0x08,
        SampleOffset = 0x09,
        VolumeSlide = 0x0A,
        PositionJump = 0x0B,
        SetVolume = 0x0C,
        PatternBreak = 0x0D,
        Extended = 0x0E,
        SetSpeed = 0X0F
    }
}
