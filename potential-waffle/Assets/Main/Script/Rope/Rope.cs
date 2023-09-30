using System.Collections;
using System.Collections.Generic;
using Shapes;
using Unity.Collections;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] Transform _shipTransform;
    [SerializeField] Transform _playerTransform;
    [SerializeField] InvertedCircleCollider2D _circleCollider;


    float ropeSegLen = 0.05f;
    int segmentAmount = 0;

    private Polyline polyLine;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();

    // Use this for initialization
    void Start()
    {
        this.polyLine = this.GetComponent<Polyline>();
        TestMaxDist();
    }

    public void RedefineRope()
    {
        ropeSegments.Clear();
        Vector3 ropeStartPoint = _shipTransform.position;

        for (int i = 0; i < segmentAmount; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    public void TestMaxDist()
    {
        float totalLength = _circleCollider.Radius;
        int newSegmentAmount = (int)(totalLength / ropeSegLen * 0.6f);

        if (segmentAmount != newSegmentAmount)
        {
            segmentAmount = newSegmentAmount;
            RedefineRope();
        }
    }


    // Update is called once per frame
    void Update()
    {
        TestMaxDist();
        this.DrawRope();
    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    private void Simulate()
    {
        // SIMULATION
        //Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < this.segmentAmount; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            //firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = _shipTransform.position;
        this.ropeSegments[0] = firstSegment;

        RopeSegment endSegment = this.ropeSegments[this.ropeSegments.Count - 1];
        endSegment.posNow = this._playerTransform.position;
        this.ropeSegments[this.ropeSegments.Count - 1] = endSegment;

        for (int i = 0; i < this.segmentAmount - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = dist - ropeSegLen;
            Vector2 changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            Vector2 changeAmount = changeDir * error;

            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    private void DrawRope()
    {
        Vector3[] ropePositions = new Vector3[this.segmentAmount];
        for (int i = 0; i < this.segmentAmount; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        polyLine.SetPoints(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}