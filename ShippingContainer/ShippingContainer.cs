﻿// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// <auto-generated />

namespace Eco.Mods.TechTree
{
      using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Modules;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(PublicStorageComponent))]                
    public partial class ShippingContainerObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Shipping Container"); } } 

        public override TableTextureMode TableTexture => TableTextureMode.Wood; 

        public virtual Type RepresentedItemType { get { return typeof(ShippingContainerItem); } }


        public class InventoryMultiply : InventoryRestriction
        {
            public override int Priority => base.Priority - 15;

            public override LocString Message => Localizer.DoStr("Inventory Full");

            public override int MaxAccepted(Item item, int currentQuantity) =>  item.MaxStackSize > 1  ? 300 : ( item.Tags().Any(x => x.Name == "Tools") ? 1 : 5);

            public override bool SurpassStackSize => true;

        }


        protected override void Initialize()
        {

            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(48);
            storage.Storage.AddInvRestriction(new NotCarriedRestriction()); // can't store block or large items
            storage.Storage.AddInvRestriction(new InventoryMultiply());
        }
       
    }

    [Serialized]
    [LocDisplayName("Shipping Container")]
    [Ecopedia("Crafted Objects", "Storage", createAsSubPage: true, displayOnPage: true)]

    public partial class ShippingContainerItem :
        WorldObjectItem<ShippingContainerObject> 
    {
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Large end game storage."); } }

        static ShippingContainerItem()
        {
            WorldObject.AddOccupancy<ShippingContainerObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 0, 0)),
            new BlockOccupancy(new Vector3i(1, 0, 0)),
            new BlockOccupancy(new Vector3i(2, 0, 0)),
            new BlockOccupancy(new Vector3i(3, 0, 0)),
            new BlockOccupancy(new Vector3i(4, 0, 0)),
            new BlockOccupancy(new Vector3i(0, 0, 1)),
            new BlockOccupancy(new Vector3i(1, 0, 1)),
            new BlockOccupancy(new Vector3i(2, 0, 1)),
            new BlockOccupancy(new Vector3i(3, 0, 1)),
            new BlockOccupancy(new Vector3i(4, 0, 1)),
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            new BlockOccupancy(new Vector3i(1, 1, 0)),
            new BlockOccupancy(new Vector3i(2, 1, 0)),
            new BlockOccupancy(new Vector3i(3, 1, 0)),
            new BlockOccupancy(new Vector3i(4, 1, 0)),
            new BlockOccupancy(new Vector3i(0, 1, 1)),
            new BlockOccupancy(new Vector3i(1, 1, 1)),
            new BlockOccupancy(new Vector3i(2, 1, 1)),
            new BlockOccupancy(new Vector3i(3, 1, 1)),
            new BlockOccupancy(new Vector3i(4, 1, 1)),
            });
        }

        

    }
    [RequiresSkill(typeof(SmeltingSkill), 4)]
    public partial class ShippingContainerRecipe :
        RecipeFamily
    {
        public ShippingContainerRecipe()
        {
            var product = new Recipe(
                "ShippingContainer",
                Localizer.DoStr("Shipping Container"),
                new IngredientElement[]
                {
               new IngredientElement(typeof(SteelBarItem), 8),
               new IngredientElement(typeof(LumberItem) , 12),
                },
               new CraftingElement<ShippingContainerItem>()
            );
            this.Recipes = new List<Recipe> { product };
            this.LaborInCalories = CreateLaborInCaloriesValue(800); 
            this.CraftMinutes = CreateCraftTimeValue(5);
            this.Initialize(Localizer.DoStr("Shipping Container"), typeof(ShippingContainerRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}