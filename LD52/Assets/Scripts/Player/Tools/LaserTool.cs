using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Splines;

public class LaserTool : Tool
{
    public Transform splineObjects;
    public LaserSpline laserSplineClose;
    public LaserSpline laserSplineFar;
    public Vector3 scaleFocussed;
    public Vector3 scaleUnfocussed;

    private Vector3 currentTargetPosition;
    private Vector3 targetPosition;
    private Vector3 targetMiddle;
    private Vector3 randomRotation;
    private bool isFocussing;

    protected override void OnUpdate()
    {
        CheckFocus();
        UpdateTarget();
        UpdateLine();
        UpdateMesh();
    }

    private void CheckFocus()
    {
        if (Game.inst.input.GetFocusDown())
        {
            isFocussing = true;
            UpdateTargetPosition();
        }
        
        if (Game.inst.input.GetFocusUp())
        {
            isFocussing = false;
            UpdateTargetPosition();
        }
    }

    private void UpdateTarget()
    {
        if(Random.value < 0.033f)
        {
            UpdateTargetPosition();
        }
        
        currentTargetPosition = Vector3.Lerp(currentTargetPosition, targetPosition, Time.deltaTime * 8f);
        if(Vector3.Distance(currentTargetPosition, targetPosition) < 1f)
        {
            UpdateTargetPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        targetMiddle = transform.position + transform.forward * GetData().range * (isFocussing ? 2f : 1f);
        targetPosition = RotatePointAroundPivot(targetMiddle, transform.position, GetRandomRotation());
    }

    private void UpdateLine()
    {
        splineObjects.localScale = isFocussing ? scaleFocussed : scaleUnfocussed;


    }

    private void UpdateMesh()
    {
        
    }

    protected override void OnCooldown()
    {
        //laserObject.toolArea.SetCollEnabled(false);
        //laserObject.toolArea.SetCollEnabled(true);
    }

    private Vector3 GetRandomRotation()
    {
        float max = isFocussing ? 3.33f : 32f;
        randomRotation = new Vector3(Random.Range(-max, max) / 8f, Random.Range(-max, max), Random.Range(-max, max));
        return randomRotation;
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
       Vector3 dir = point - pivot; // get point direction relative to pivot
       dir = Quaternion.Euler(angles)* dir; // rotate it
       point = dir + pivot; // calculate rotated point
       return point; // return it
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            //Gizmos.DrawSphere(targetPosition, 0.33f);
        }
    }
}
