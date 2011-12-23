﻿// Copyright 2008 - Paul den Dulk (Geodan)
// 
// This file is part of SharpMap.
// SharpMap is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpMap is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System.Drawing;
using BruTile;

namespace WinFormsSample
{
    class MapTransform
    {
        #region Fields

        float _resolution;
        PointF _center;
        float _width;
        float _height;
        Extent _extent;

        #endregion

        #region Public Methods

        public MapTransform(PointF center, float resolution, float width, float height)
        {
            _center = center;
            _resolution = resolution;
            _width = width;
            _height = height;
            UpdateExtent();
        }

        public float Resolution
        {
            set
            {
                _resolution = value;
                UpdateExtent();
            }
            get
            {
                return _resolution;
            }
        }

        public PointF Center
        {
            set
            {
                _center = value;
                UpdateExtent();
            }
            get { return _center; }
        }

        public float Width
        {
            set
            {
                _width = value;
                UpdateExtent();
            }
        }

        public float Height
        {
            set
            {
                _height = value;
                UpdateExtent();
            }
        }

        public Extent Extent
        {
            get { return _extent; }
        }

        public PointF WorldToMap(double x, double y)
        {
            return new PointF((float)(x - _extent.MinX) / _resolution, (float)(_extent.MaxY - y) / _resolution);
        }

        public PointF MapToWorld(double x, double y)
        {
            return new PointF((float)(_extent.MinX + (x * _resolution)), (float)(_extent.MaxY -( y * _resolution)));
        }

        public RectangleF WorldToMap(Extent extent)
        {
            return WorldToMap(extent.MinX, extent.MinY, extent.MaxX, extent.MaxY);
        }

        public RectangleF WorldToMap(double x1, double y1, double x2, double y2)
        {
            PointF point1 = WorldToMap(x1, y1);
            PointF point2 = WorldToMap(x2, y2);
            return new RectangleF(point1.X, point2.Y, point2.X - point1.X, point1.Y - point2.Y);
        }

        #endregion

        #region Private Methods

        private void UpdateExtent()
        {
            float spanX = _width * _resolution;
            float spanY = _height * _resolution;
            _extent = new Extent(
                _center.X - spanX * 0.5f, _center.Y - spanY * 0.5f,
                _center.X + spanX * 0.5f, _center.Y + spanY * 0.5f);
        }

        #endregion
    }
}