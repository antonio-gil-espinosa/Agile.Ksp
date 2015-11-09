using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

namespace Agile.Ksp.Extensions
{
    public static class PartExtensions
    {
        public static bool TryRequestResource(this Part part, string resourceName, double quantity, ResourceFlowMode resourceFlowMode)
        {
            return TryRequestResource(part, PartResourceLibrary.Instance.GetDefinition(resourceName), quantity, resourceFlowMode);
        }

        public static bool TryRequestResource(this Part part, int resourceId, double quantity, ResourceFlowMode resourceFlowMode)
        {
            return TryRequestResource(part, PartResourceLibrary.Instance.GetDefinition(resourceId), quantity, resourceFlowMode);
        }

        public static bool TryRequestResource(this Part part, PartResourceDefinition partResourceDefinition, double quantity, ResourceFlowMode resourceFlowMode)
        {
            var ret = HasResource(part, partResourceDefinition, quantity, resourceFlowMode);
            if (ret)
                part.RequestResource(partResourceDefinition.id, quantity);
            return ret;
        }

        public static bool HasResource(this Part part, PartResourceDefinition partResourceDefinition, double quantity, ResourceFlowMode resourceFlowMode)
        {
            List<PartResource> partResources = new List<PartResource>();
            part.GetConnectedResources(partResourceDefinition.id, resourceFlowMode, partResources);
            double available = partResources.Sum(x => x.amount);

            return available >= quantity;
        }

        public static bool HasResource(this Part part, int resourceId, double quantity, ResourceFlowMode resourceFlowMode)
        {
            return HasResource(part, PartResourceLibrary.Instance.GetDefinition(resourceId), quantity, resourceFlowMode);
        }

        public static bool HasResource(this Part part, string resourceName, double quantity, ResourceFlowMode resourceFlowMode)
        {
            return HasResource(part, PartResourceLibrary.Instance.GetDefinition(resourceName), quantity, resourceFlowMode);
        }

        public static bool TryRequestResource(this Part part, string resourceName, double quantity)
        {
            return TryRequestResource(part, PartResourceLibrary.Instance.GetDefinition(resourceName), quantity);
        }

        public static bool TryRequestResource(this Part part, int resourceId, double quantity)
        {
            return TryRequestResource(part, PartResourceLibrary.Instance.GetDefinition(resourceId), quantity);
        }

        public static bool TryRequestResource(this Part part, PartResourceDefinition partResourceDefinition, double quantity)
        {
            return TryRequestResource(part, partResourceDefinition, quantity, partResourceDefinition.resourceFlowMode);
        }

        public static float GetVolume(this Part partPrefab)
        {
            Bounds[] rendererBounds = partPrefab.GetRendererBounds();
            Vector3 boundsSize = PartGeometryUtil.MergeBounds(rendererBounds, partPrefab.transform).size;
            float volume = boundsSize.x * boundsSize.y * boundsSize.z;
            return volume * 1000;
        }

        public static BaseEvent GetEvent<T>(this T partModule, Expression<Func<T, object>> expression) where T : PartModule
        {
            return partModule.Events[expression.ToMethod().Name];
        }

        public static BaseField GetField<T>(this T partModule, Expression<Func<T, object>> expression) where T : PartModule
        {
            return partModule.Fields[expression.ToField().Name];
        }
    }
}