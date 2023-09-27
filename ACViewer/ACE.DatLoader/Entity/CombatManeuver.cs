using System.IO;
using ACE.Entity.Enum;

namespace ACE.DatLoader.Entity
{
    public class CombatManeuver : IUnpackable
    {
        public MotionStance Style { get; set; }
        public AttackHeight AttackHeight { get; set; }
        public AttackType AttackType { get; set; }
        public uint MinSkillLevel { get; set; }
        public MotionCommand Motion { get; set; }

        public void Unpack(BinaryReader reader)
        {
            Style           = (MotionStance)reader.ReadUInt32();
            AttackHeight    = (AttackHeight)reader.ReadUInt32();
            AttackType      = (AttackType)reader.ReadUInt32();
            MinSkillLevel   = reader.ReadUInt32();
            Motion          = (MotionCommand)reader.ReadUInt32();
        }
    }
}
