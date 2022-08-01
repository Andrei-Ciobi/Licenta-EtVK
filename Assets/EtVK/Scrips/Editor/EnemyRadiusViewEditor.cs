using EtVK.AI_Module.Core;
using UnityEditor;
using UnityEngine;
// ReSharper disable CheckNamespace

    [CustomEditor(typeof(EnemyManager))]
    public class EnemyRadiusViewEditor : Editor
    {
        
        private void OnSceneGUI()
        {
            var fow = (EnemyManager)target;
            
            if(fow.GetLocomotionData() == null)
                return;
            
            var index = 2;
            var angle = fow.GetLocomotionData().DetectionAngle;
            for (var i = 3; i > 1; i--)
            {
                Handles.color = Color.white;
                var viewAngleA = fow.DirectionFromAngle(-angle/ index, false);
                var viewAngleB = fow.DirectionFromAngle(angle / index, false);

                Handles.DrawLine(fow.transform.position,
                    fow.transform.position + viewAngleA * fow.GetLocomotionData().BaseDetectionRadius, 1.5f);
                Handles.DrawLine(fow.transform.position,
                    fow.transform.position + viewAngleB * fow.GetLocomotionData().BaseDetectionRadius, 1.5f);
                index *= i;
            }
            
            // Detection range radius
            Handles.color = Color.blue;
            Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360,
                fow.GetLocomotionData().BaseDetectionRadius, 1.5f);
            
            // Attack range radius
            Handles.color = Color.blue;
            Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360,
                fow.GetLocomotionData().BaseAttackRadius, 1.5f);
            
            // Melee range radius
            Handles.color = Color.green;
            Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360,
                fow.GetLocomotionData().BaseMeleeRadius, 1.5f);
            
            // Aggro range radius
            Handles.color = Color.red;
            Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360,
                fow.GetLocomotionData().AggroRange, 1.5f);
            
            // Combat aggro range radius
            Handles.color = Color.yellow;
            Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360,
                fow.GetLocomotionData().CombatAggroRange, 1.5f);




        }
    }