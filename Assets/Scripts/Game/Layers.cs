using UnityEngine;

public static class Layers {
    public static class Names {
        public const string Ground = "Ground";
        public const string Actor = "Actor";
        public const string DamageObstracle = "DamageObstracle";
    }

    public static class Masks {
        static Masks() {
            Ground = LayerMask.GetMask(Names.Ground);
            Actor = LayerMask.GetMask(Names.Actor); ;
            DamageObstracle = LayerMask.GetMask(Names.DamageObstracle);
            Damageable = Ground + Actor + DamageObstracle;
        }

        public static int Ground { get; private set; }
        public static int Actor { get; private set; }
        public static int Damageable { get; private set; }
        public static int DamageObstracle { get; private set; }
    }
}
