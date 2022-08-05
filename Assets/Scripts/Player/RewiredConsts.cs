// <auto-generated>
// Rewired Constants
// This list was generated on 8/5/2022 12:00:43 PM
// The list applies to only the Rewired Input Manager from which it was generated.
// If you use a different Rewired Input Manager, you will have to generate a new list.
// If you make changes to the exported items in the Rewired Input Manager, you will
// need to regenerate this list.
// </auto-generated>

namespace MeltingChamber.ReConsts {
    public static partial class Action {
        // Default
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Default", friendlyName = "MoveHorizontal")]
        public const int MoveHorizontal = 0;
        
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Default", friendlyName = "MoveVertical")]
        public const int MoveVertical = 1;
        
        // Gameplay
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Gameplay", friendlyName = "Dash")]
        public const int Dash = 2;
        
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Gameplay", friendlyName = "Reflect")]
        public const int Reflect = 3;
        
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Gameplay", friendlyName = "AimHorizontal")]
        public const int AimHorizontal = 4;
        
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Gameplay", friendlyName = "AimVertical")]
        public const int AimVertical = 5;
        
        // UI
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "UI", friendlyName = "Pause")]
        public const int Pause = 6;
        
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "UI", friendlyName = "Confirm")]
        public const int Confirm = 7;
        
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "UI", friendlyName = "Cancel")]
        public const int Cancel = 8;
        
    }
    public static partial class Category {
        public const int Default = 0;
        
        public const int Gameplay = 1;
        
        public const int UI = 2;
        
    }
    public static partial class Layout {
        public static partial class Joystick {
            public const int Default = 0;
            
        }
        public static partial class Keyboard {
            public const int Default = 0;
            
        }
        public static partial class Mouse {
            public const int Default = 0;
            
        }
        public static partial class CustomController {
            public const int Default = 0;
            
        }
    }
    public static partial class Player {
        [Rewired.Dev.PlayerIdFieldInfo(friendlyName = "System")]
        public const int System = 9999999;
        
        [Rewired.Dev.PlayerIdFieldInfo(friendlyName = "Player0")]
        public const int Player0 = 0;
        
    }
    public static partial class CustomController {
        public static partial class TopDownMouse {
            public const int sourceId = 0;
            
            public const string name = "TopDownMouse";
            
            public static readonly System.Guid typeGuid = new System.Guid("c1126ddd-1d3c-4296-b67e-b52cf4eda805");
            
            public static partial class Axis {
                public const int Horizontal = 0;
                
                public const int Vertical = 1;
                
            }
        }
    }
    public static partial class LayoutManagerRuleSet {
    }
    public static partial class MapEnablerRuleSet {
    }
}
