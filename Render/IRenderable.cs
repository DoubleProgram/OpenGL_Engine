﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public interface IRenderable {
        bool isRenderable { get; set; }
        void Render(Object obj);
    }
}