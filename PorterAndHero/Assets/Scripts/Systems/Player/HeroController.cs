using Interfaces;
using Systems.Items;

namespace Systems.Player
{
    public class HeroController : PlayerController
    {
        private Item _item;
        
        protected override void AssignControls()
        {
            base.AssignControls();
            
        }
        
        public void UseItem()
        {
            _item.Use(new UseContext());
        }

        public void DropItem()
        {
            
        }
    }
}
