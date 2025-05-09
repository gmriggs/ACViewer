using ACE.Entity.Enum;

namespace ACE.Server.WorldObjects
{
    partial class Creature
    {
        /// <summary>
        /// The list of combat maneuvers performable by this creature
        /// </summary>
        public DatReaderWriter.DBObjs.CombatTable CombatTable { get; set; }

        public CombatMode CombatMode { get; protected set; }
    }
}
