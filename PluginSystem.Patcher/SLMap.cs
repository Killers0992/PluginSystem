using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using LightContainmentZoneDecontamination;
using MapGeneration;
using PluginSystem.API;
using RemoteAdmin;
using Respawning;
using UnityEngine;

namespace PluginSystem
{
    public class SLMap : Map
    {
        private System.Random rng = new System.Random();

        private AlphaWarheadOutsitePanel _outsidePanel;
        public AlphaWarheadOutsitePanel OutsidePanel
        {
            get
            {
                if (_outsidePanel == null)
                    _outsidePanel = UnityEngine.Object.FindObjectOfType<AlphaWarheadOutsitePanel>();

                return _outsidePanel;
            }
        }

        private Broadcast _broadcast;
        public Broadcast BroadCast
        {
            get
            {
                if (_broadcast == null)
                    _broadcast = QueryProcessor.Localplayer.GetComponent<Broadcast>();

                return _broadcast;
            }
        }


        public override bool WarheadDetonated => AlphaWarheadController.Host.detonated;

        public override bool LCZDecontaminated => DecontaminationController.Singleton.IsDecontaminating;

        public override bool WarheadLeverEnabled
        {
            get
            {
                return AlphaWarheadOutsitePanel.nukeside.Networkenabled;
            }
            set
            {
                AlphaWarheadOutsitePanel.nukeside.Networkenabled = value;
            }
        }

        public override bool WarheadKeycardEntered 
        {
            get
            {
                return OutsidePanel.NetworkkeycardEntered;
            }
            set
            {
                OutsidePanel.NetworkkeycardEntered = value;
            }
        }

        public override void AnnounceCustomMessage(string words)
        {
            RespawnEffectsController.PlayCassieAnnouncement(words, true, true);
        }

        public override void AnnounceCustomMessage(string words, bool makeHold, bool makeNoise)
        {
            RespawnEffectsController.PlayCassieAnnouncement(words, makeHold, makeNoise);
        }

        public override void Broadcast(string message, ushort duration, bool isMonoSpaced)
        {
            BroadCast.RpcAddElement(message, duration, isMonoSpaced ? global::Broadcast.BroadcastFlags.Monospaced : global::Broadcast.BroadcastFlags.Normal);
        }

        public override void ClearBroadcasts()
        {
            BroadCast.RpcClearElements();
        }

        public override void DetonateWarhead()
        {
            AlphaWarheadController.Host.InstantPrepare();
            AlphaWarheadController.Host.StartDetonation(false);
            AlphaWarheadController.Host.NetworktimeToDetonation = 0.1f;
        }

        public override Player GetIntercomSpeaker()
        {
            var hub = ReferenceHub.GetHub(Intercom.host.speaker);
            if (hub == null)
                return null;

            var plr = SLPlayer.GetOrAdd(hub);
            return plr;
        }

        public override Vector GetRandomSpawnPoint(IRole role)
        {
            var spawnPoints = SpawnpointManager.Positions[(RoleType)role.RoleId];

            var rngSpawnPoint = spawnPoints[rng.Next(0, spawnPoints.Length - 1)];

            return new Vector(
                rngSpawnPoint.transform.position.x,
                rngSpawnPoint.transform.position.y,
                rngSpawnPoint.transform.position.z);
        }

        public override void OverchargeLights(float forceDuration, bool onlyHeavy)
        {
            foreach (var light in FlickerableLightController.Instances)
            {
                if (RoomIdentifier.RoomsByCoordinatess.TryGetValue(RoomIdUtils.PositionToCoords(light.transform.position), out RoomIdentifier room))
                {
                    if (onlyHeavy && room.Zone == FacilityZone.HeavyContainment)
                        light.ServerFlickerLights(forceDuration);
                    else if (!onlyHeavy)
                        light.ServerFlickerLights(forceDuration);
                }
            }
        }

        public override void SetIntercomSpeaker(Player player)
        {
            Intercom.host.RequestTransmission((GameObject)player.GetGameObject());
        }

        public override void Shake()
        {
            AlphaWarheadController.Host.RpcShake(false);
        }

        public override void SpawnItem(API.ItemType type, Vector position, Vector rotation)
        {
            if (!InventoryItemLoader.AvailableItems.TryGetValue((ItemType)type, out ItemBase itemBase))
                return;

            if (!ReferenceHub.TryGetLocalHub(out ReferenceHub referenceHub))
                return;

            PickupSyncInfo psi = new PickupSyncInfo
            {
                ItemId = (ItemType)type,
                Serial = ItemSerialGenerator.GenerateNext(),
                Weight = itemBase.Weight
            };

            ItemPickupBase itemPickupBase = referenceHub.inventory.ServerCreatePickup(itemBase, psi, true);
            itemPickupBase.transform.position = new Vector3(position.x, position.y, position.z);
            itemPickupBase.transform.eulerAngles = new Vector3(rotation.x, rotation.y, rotation.z);
            itemPickupBase.RefreshPositionAndRotation();
        }

        public override void StartWarhead()
        {
            AlphaWarheadController.Host.StartDetonation(false);
        }

        public override void StopWarhead()
        {
            AlphaWarheadController.Host.CancelDetonation(null);
        }
    }
}
