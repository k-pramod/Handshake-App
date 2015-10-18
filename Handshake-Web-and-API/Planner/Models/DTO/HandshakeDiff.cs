using Planner.Models.Database;

namespace Planner.Models.DTO
{
    public class HandshakeDiff
    {
        public Handshake handshakeOne { get; set; }
        public Handshake handshakeTwo { get; set; }
        public int distanceBetween { get; set; }
        public int timeDiffBetween { get; set; }
    }
}