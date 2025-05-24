using System;
using Interfaces;
using Scriptables.Entities;
using Systems.Player;
using UnityEngine;

namespace Systems.Items
{
    public class Treasure : Item
    {
        protected override void OnUse(UseContext useContext)
        {
            if (useContext.SourceEntity.EntityDatum.EntityType != EntityDatum.Type.Player) return;
            var playerController = useContext.SourceGameObject.GetComponent<PlayerController>();
            
            if (playerController.PlayerType == PlayerController.Type.Hero) Throw(playerController.GetAimInput());
            else playerController.Inventory.ConsumeItem(this);
        }

        protected override void OnStopUsing(UseContext useContext)
        {

        }

        protected override void OnCombineWith(Item item)
        {
            throw new System.NotImplementedException();
        }
    }
}
