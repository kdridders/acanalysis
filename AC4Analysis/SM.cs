﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
namespace AC4Analysis
{
    public partial class SM : UserControl
    {
        public byte[] data;
        bool OpenLight = false;
        List<Single> Verts = new List<Single>();
        List<Single> Normals = new List<Single>();
        List<Single> TexCoords = new List<Single>();

        [DllImport("AC4_3DWIN.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Set3DData(float[] VecsIn, float[] NorsIn, int VecSizeIn, float[] TexsIn, int TexsSizeIn);
        [DllImport("AC4_3DWIN.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void LightSwitch(bool Use);
        public SM()
        {
            InitializeComponent();
            btn开关灯.Text = "Turn On Light";
        }
        public IntPtr GetHwnd()
        {
            return panel1.Handle;
        }
        public void Analysis_SM()
        {
            if (data == null)
                return;
            Verts = new List<Single>();
            Normals = new List<Single>();
            TexCoords = new List<Single>();
            Int32 mOffset = 0;
            mOffset = BitConverter.ToInt32(data, 20);              // Model.Offset
            mOffset = BitConverter.ToInt32(data, mOffset + 36);    // RootPart.Offset
            mOffset = BitConverter.ToInt32(data, mOffset + 4);     // RootPart.Vertex.Offset

            while (mOffset < data.Length)
            {
                if (data[mOffset] == 0x0A)
                {
                    for (Int32 i = 2; i > -1; i--)
                    {
                        Int32 mTexCoordsOffset = mOffset + 20 + i * 8;      // UV数据起始位置
                        TexCoords.Add(-(Single)BitConverter.ToInt16(data, mTexCoordsOffset) / 0x1000);
                        TexCoords.Add(-(Single)BitConverter.ToInt16(data, mTexCoordsOffset + 2) / 0x1000);

                        Int32 mNormalsOffset = mOffset + 144 + i * 8;        // Normal数据起始位置
                        Normals.Add(-(Single)BitConverter.ToInt16(data, mNormalsOffset) / 0x1000);
                        Normals.Add(-(Single)BitConverter.ToInt16(data, mNormalsOffset + 2) / 0x1000);
                        Normals.Add(-(Single)BitConverter.ToInt16(data, mNormalsOffset + 4) / 0x1000);

                        Int32 mVertsOffset = mOffset + 76 + i * 16;        // Vertex数据起始位置
                        Verts.Add(BitConverter.ToSingle(data, mVertsOffset));
                        Verts.Add(BitConverter.ToSingle(data, mVertsOffset + 4));
                        Verts.Add(BitConverter.ToSingle(data, mVertsOffset + 8));                        
                    }
                    
                    for (Int32 i = 1; i < 4 ; i++)
                    {
                        Int32 mTexCoordsOffset = mOffset + 20 + i * 8;      // UV数据起始位置
                        TexCoords.Add(-(Single)BitConverter.ToInt16(data, mTexCoordsOffset) / 0x1000);
                        TexCoords.Add(-(Single)BitConverter.ToInt16(data, mTexCoordsOffset + 2) / 0x1000);

                        Int32 mNormalsOffset = mOffset + 144 + i * 8;        // Normal数据起始位置
                        Normals.Add(-(Single)BitConverter.ToInt16(data, mNormalsOffset) / 0x1000);
                        Normals.Add(-(Single)BitConverter.ToInt16(data, mNormalsOffset + 2) / 0x1000);
                        Normals.Add(-(Single)BitConverter.ToInt16(data, mNormalsOffset + 4) / 0x1000);

                        Int32 mVertsOffset = mOffset + 76 + i * 16;        // Vertex数据起始位置
                        Verts.Add(BitConverter.ToSingle(data, mVertsOffset));
                        Verts.Add(BitConverter.ToSingle(data, mVertsOffset + 4));
                        Verts.Add(BitConverter.ToSingle(data, mVertsOffset + 8));
                    }
                    /*
                    for (Int32 i = 0; i < 4; i++)
                    {
                        Int32 mTexCoordsOffset = mOffset + 20 + i * 8;      // UV数据起始位置
                        TexCoords.Add((Single)BitConverter.ToInt16(data, mTexCoordsOffset) / 0x1000);
                        TexCoords.Add((Single)BitConverter.ToInt16(data, mTexCoordsOffset + 2) / 0x1000);

                        Int32 mNormalsOffset = mOffset + 144 + i * 8;        // Normal数据起始位置
                        Normals.Add((Single)BitConverter.ToInt16(data, mNormalsOffset) / 0x7FFF);
                        Normals.Add((Single)BitConverter.ToInt16(data, mNormalsOffset + 2) / 0x1000);
                        Normals.Add((Single)BitConverter.ToInt16(data, mNormalsOffset + 4) / 0x1000);

                        Int32 mVertsOffset = mOffset + 76 + i * 16;        // Vertex数据起始位置
                        Verts.Add(BitConverter.ToSingle(data, mVertsOffset));
                        Verts.Add(BitConverter.ToSingle(data, mVertsOffset + 4));
                        Verts.Add(BitConverter.ToSingle(data, mVertsOffset + 8));
                    }*/
                    mOffset += 256;
                }
                else
                {
                    if(data[mOffset] == 0x08)
                    {
                        for (Int32 i = 2; i > -1; i--)
                        {
                            Int32 mTexCoordsOffset = mOffset + 24 + i * 8;      // UV数据起始位置
                            TexCoords.Add(-(Single)BitConverter.ToInt16(data, mTexCoordsOffset) / 0x1000);
                            TexCoords.Add(-(Single)BitConverter.ToInt16(data, mTexCoordsOffset + 2) / 0x1000);

                            Int32 mNormalsOffset = mOffset + 120 + i * 8;        // Normal数据起始位置
                            Normals.Add(-(Single)BitConverter.ToInt16(data, mNormalsOffset) / 0x1000);
                            Normals.Add(-(Single)BitConverter.ToInt16(data, mNormalsOffset + 2) / 0x1000);
                            Normals.Add(-(Single)BitConverter.ToInt16(data, mNormalsOffset + 4) / 0x1000);

                            Int32 mVertsOffset = mOffset + 68 + i * 16;        // Vertex数据起始位置
                            Verts.Add(BitConverter.ToSingle(data, mVertsOffset));
                            Verts.Add(BitConverter.ToSingle(data, mVertsOffset + 4));
                            Verts.Add(BitConverter.ToSingle(data, mVertsOffset + 8));
                        }
                        mOffset += 224;
                    }
                    else
                    {
                        mOffset += 0x10;    // 未完善，暴力查找
                    }
                }
            }
            float[] Vesout = new float[Verts.Count];
            float[] Norout = new float[Verts.Count];
            float[] Texout = new float[TexCoords.Count];
            for (int i = 0; i < TexCoords.Count; i++)
            {
                Texout[i] = TexCoords[i];
            }
            for (int i = 0; i < Verts.Count; i++)
            {
                Vesout[i] = Verts[i];
                Norout[i] = Normals[i];
            }
            Set3DData(Vesout, Norout, Vesout.Length, Texout, TexCoords.Count);
        }

        private void btn开关灯_Click(object sender, EventArgs e)
        {
            OpenLight = !OpenLight;
            if (OpenLight)
                btn开关灯.Text = "Turn Off Light";
            else
                btn开关灯.Text = "Turn On Light";
            LightSwitch(OpenLight);
        }
    }
}
