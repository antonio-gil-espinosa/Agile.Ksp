using UnityEngine;

namespace Agile.Ksp.Extensions
{
    public static class AvailablaPartExtensions
    {
        public static Part SpawnPart(this AvailablePart availablePart, Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity)
        {
            ProtoPartSnapshot snapshot = new ProtoPartSnapshot(availablePart.partPrefab, null);

            if (HighLogic.CurrentGame.flightState.ContainsFlightID(snapshot.flightID) || snapshot.flightID == 0)
            {
                snapshot.flightID = ShipConstruction.GetUniqueFlightID(HighLogic.CurrentGame.flightState);
            }

            snapshot.parentIdx = 0;
            snapshot.position = position;
            snapshot.rotation = rotation;
            snapshot.stageIndex = 0;
            snapshot.defaultInverseStage = 0;
            snapshot.seqOverride = -1;
            snapshot.inStageIndex = -1;
            snapshot.attachMode = (int)AttachModes.SRF_ATTACH;
            snapshot.attached = true;
            snapshot.connected = true;

            Part newPart = snapshot.Load(FlightGlobals.ActiveVessel, false);

            newPart.transform.position = position;
            newPart.transform.rotation = rotation;
            FlightGlobals.ActiveVessel.Parts.Add(newPart);

            newPart.physicalSignificance = Part.PhysicalSignificance.NONE;
            newPart.PromoteToPhysicalPart();
            newPart.Unpack();
            newPart.InitializeModules();

            newPart.rigidbody.velocity = velocity;
            newPart.rigidbody.angularVelocity = angularVelocity;

            newPart.decouple();
            newPart.vessel.vesselType = VesselType.Unknown;
            newPart.vessel.vesselName = availablePart.title;

            return newPart;
        }
    }
}