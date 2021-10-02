﻿using System;
using CUE4Parse.UE4.Readers;
using CUE4Parse.Utils;

namespace CUE4Parse.UE4.Objects.Core.Math
{
    // Generic axis enum (mirrored for property use in Object.h)
    public enum EAxis
    {
        None,
        X,
        Y,
        Z,
    }

    public class FMatrix : IUStruct
    {
        public float M00, M01, M02, M03;
        public float M10, M11, M12, M13;
        public float M20, M21, M22, M23;
        public float M30, M31, M32, M33;

        public FMatrix() {}
        public FMatrix(FArchive Ar)
        {
            M00 = Ar.Read<float>();
            M01 = Ar.Read<float>();
            M02 = Ar.Read<float>();
            M03 = Ar.Read<float>();
            M10 = Ar.Read<float>();
            M11 = Ar.Read<float>();
            M12 = Ar.Read<float>();
            M13 = Ar.Read<float>();
            M20 = Ar.Read<float>();
            M21 = Ar.Read<float>();
            M22 = Ar.Read<float>();
            M23 = Ar.Read<float>();
            M30 = Ar.Read<float>();
            M31 = Ar.Read<float>();
            M32 = Ar.Read<float>();
            M33 = Ar.Read<float>();
        }

        public static FMatrix operator *(FMatrix a, FMatrix b) => new()
        {
            M00 = a.M00 * b.M00 + a.M01 * b.M10 + a.M02 * b.M20 + a.M03 * b.M30,
            M01 = a.M00 * b.M01 + a.M01 * b.M11 + a.M02 * b.M21 + a.M03 * b.M31,
            M02 = a.M00 * b.M02 + a.M01 * b.M12 + a.M02 * b.M22 + a.M03 * b.M32,
            M03 = a.M00 * b.M03 + a.M01 * b.M13 + a.M02 * b.M23 + a.M03 * b.M33,
            M10 = a.M10 * b.M00 + a.M11 * b.M10 + a.M12 * b.M20 + a.M13 * b.M30,
            M11 = a.M10 * b.M01 + a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31,
            M12 = a.M10 * b.M02 + a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32,
            M13 = a.M10 * b.M03 + a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33,
            M20 = a.M20 * b.M00 + a.M21 * b.M10 + a.M22 * b.M20 + a.M23 * b.M30,
            M21 = a.M20 * b.M01 + a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31,
            M22 = a.M20 * b.M02 + a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32,
            M23 = a.M20 * b.M03 + a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33,
            M30 = a.M30 * b.M00 + a.M31 * b.M10 + a.M32 * b.M20 + a.M33 * b.M30,
            M31 = a.M30 * b.M01 + a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31,
            M32 = a.M30 * b.M02 + a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32,
            M33 = a.M30 * b.M03 + a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33
        };

        public FVector4 TransformFVector4(FVector4 p) => new(
            p.X * M00 + p.Y * M10 + p.Z * M20 + p.W * M30,
            p.X * M01 + p.Y * M11 + p.Z * M21 + p.W * M31,
            p.X * M02 + p.Y * M12 + p.Z * M22 + p.W * M32,
            p.X * M03 + p.Y * M13 + p.Z * M23 + p.W * M33
        );

        public FVector4 TransformVector(FVector v) => TransformFVector4(new FVector4(v.X, v.Y, v.Z, 0f));

        public FMatrix GetTransposed() => new()
        {
            M00 = M00, M01 = M10, M02 = M20, M03 = M30,
            M10 = M01, M11 = M11, M12 = M21, M13 = M31,
            M20 = M02, M21 = M12, M22 = M22, M23 = M32,
            M30 = M03, M31 = M13, M32 = M23, M33 = M33
        };

        public void RemoveScaling(float tolerance = FVector.SmallNumber)
        {
            // For each row, find magnitude, and if its non-zero re-scale so its unit length.
            var squareSum0 = (M00 * M00) + (M01 * M01) + (M02 * M02);
            var squareSum1 = (M10 * M10) + (M11 * M11) + (M12 * M12);
            var squareSum2 = (M20 * M20) + (M21 * M21) + (M22 * M22);

            //FloatSelect: return Comparand >= 0.f ? ValueGEZero : ValueLTZero;
            var scale0 = (squareSum0 - tolerance) >= 0 ? squareSum0.InvSqrt() : 1;
            var scale1 = (squareSum1 - tolerance) >= 0 ? squareSum1.InvSqrt() : 1;
            var scale2 = (squareSum2 - tolerance) >= 0 ? squareSum2.InvSqrt() : 1;

            M00 *= scale0; 
            M01 *= scale0; 
            M02 *= scale0; 
            M10 *= scale1; 
            M11 *= scale1; 
            M12 *= scale1; 
            M20 *= scale2; 
            M21 *= scale2; 
            M22 *= scale2;
        }

        public FVector GetOrigin() => new(M30, M31, M32);

        public FVector GetScaledAxis(EAxis axis) => axis switch
        {
            EAxis.X => new FVector(M00, M01, M02),
            EAxis.Y => new FVector(M10, M11, M12),
            EAxis.Z => new FVector(M20, M21, M22),
            _ => FVector.ZeroVector
        };

        public void SetAxis(int i, FVector axis)
        {
            switch (i)
            {
                case 0:
                    M00 = axis.X;
                    M01 = axis.Y;
                    M02 = axis.Z;
                    break;
                case 1:
                    M10 = axis.X;
                    M11 = axis.Y;
                    M12 = axis.Z;
                    break;
                case 2:
                    M20 = axis.X;
                    M21 = axis.Y;
                    M22 = axis.Z;
                    break;
                case 3:
                    M30 = axis.X;
                    M31 = axis.Y;
                    M32 = axis.Z;
                    break;
            }
        }

        public override string ToString() =>
            $"[{M00:F1} {M01:F1} {M02:F1} {M03:F1}] [{M10:F1} {M11:F1} {M12:F1} {M13:F1}] [{M20:F1} {M21:F1} {M22:F1} {M23:F1}] [{M30:F1} {M31:F1} {M32:F1} {M33:F1}]";

        public FRotator Rotator()
        {
            var xAxis = GetScaledAxis(EAxis.X);
            var yAxis = GetScaledAxis(EAxis.Y);
            var zAxis = GetScaledAxis(EAxis.Z);

            var rotator = new FRotator(
                MathF.Atan2(xAxis.Z, MathF.Sqrt(xAxis.X*xAxis.X + xAxis.Y*xAxis.Y)) * 180.0f / MathF.PI,
                MathF.Atan2(xAxis.Y, xAxis.X) * 180.0f / MathF.PI,
                0
            );

            var syAxis = new FRotationMatrix(rotator).GetScaledAxis(EAxis.Y);
            rotator.Roll = MathF.Atan2(zAxis | syAxis, yAxis | syAxis) * 180.0f / MathF.PI;

            return rotator;
        }
    }
}