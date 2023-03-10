// ---------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.SPAL.Storage.EntityFramework.Models.Exceptions
{
    public class NullEntityException : Xeption
    {
        public NullEntityException()
            : base(message: "Student is null.")
        { }
    }
}