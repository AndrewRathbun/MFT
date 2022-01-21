﻿using System;
using System.Text;

namespace MFT.Attributes;

public class Data : Attribute
{
    public Data(byte[] rawBytes) : base(rawBytes)
    {
        if (IsResident)
        {
            var content = new byte[AttributeContentLength];

            Buffer.BlockCopy(rawBytes, ContentOffset, content, 0, AttributeContentLength);

            ResidentData = new ResidentData(content);
        }
        else
        {
            NonResidentData = new NonResidentData(rawBytes);
        }
    }

    public ResidentData ResidentData { get; }
    public NonResidentData NonResidentData { get; }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("**** DATA ****");

        sb.AppendLine(base.ToString());

        sb.AppendLine();

        if (ResidentData == null)
        {
            sb.AppendLine("Non Resident Data");
            sb.AppendLine(NonResidentData.ToString());
        }
        else
        {
            sb.AppendLine("Resident Data");
            sb.AppendLine(ResidentData.ToString());
        }

        return sb.ToString();
    }
}