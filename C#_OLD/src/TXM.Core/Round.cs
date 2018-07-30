using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TXM.Core
{
    [Serializable]
    public class Round : ISerializable
    {
        private int version = 0;

        public List<Pairing> Pairings { get; set; }
        public List<Player> Participants { get; set; }

        public Round(List<Pairing> pairings, List<Player> participants)
        {
            Pairings = pairings;
            Participants = participants;
        }

        public Round(SerializationInfo info, StreamingContext context)
        {
            version = (int)info.GetValue("Round_Version", typeof(int));
            if (version == 0)
            {
                Pairings = (List<Pairing>)info.GetValue("Round_Pairings", typeof(List<Pairing>));
                Participants = (List<Player>)info.GetValue("Round_Participants", typeof(List<Player>));
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Round_Version", version, typeof(int));
            info.AddValue("Round_Pairings", Pairings, typeof(List<Pairing>));
            info.AddValue("Round_Participants", Participants, typeof(List<Player>));
        }
    }
}
