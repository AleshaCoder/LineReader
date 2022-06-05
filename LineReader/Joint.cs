using System.Collections.Generic;
using System.Drawing;

namespace LineReader
{
    class Joint
    {
        private Point _point;
        private List<Joint> _nearestJoints;
        private bool _first;

        public Point Point => _point;
        public bool First => _first;
        public IReadOnlyCollection<Joint> NearestJoints => _nearestJoints;

        public Joint(Point point)
        {
            _nearestJoints = new List<Joint>();
            _point = point;
        }

        public void SetNearestJoints(List<Joint> joints)
        {
            //-1,1 | 0,1 | 1,1
            //-1,0 | 0,0 | 1,0
            //-1,-1| 0,-1| 1,-1
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0) // center point
                        continue;
                    FindAndAddJoints(joints, x, y);
                }
            }
        }

        private void FindAndAddJoints(List<Joint> joints, int x, int y)
        {
            foreach (var joint in joints)
            {
                if (joint.With(_point.X + x, _point.Y + y))
                {
                    _nearestJoints.Add(joint);
                }
            }
        }

        private bool With(int x, int y)
        {
            return _point.X == x && _point.Y == y;
        }

        public void DestroyNearestJoints(Joint nearestJoint)
        {
            _nearestJoints.Remove(nearestJoint);
            foreach (var item in _nearestJoints)
            {
                item.Remove(nearestJoint);
            }
        }

        private void Remove(Joint nearestJoint)
        {
            _nearestJoints.Remove(nearestJoint);
        }

        public bool ConsistAllJoints(Joint nearestJoint)
        {
            foreach (var joint in nearestJoint.NearestJoints)
            {
                if (joint._point == _point)
                    continue;
                if (_nearestJoints.Contains(joint) == false)
                    return false;
            }
            return true;
        }

        public void SetFirst()
        {
            _first = true;
        }
    }
}
