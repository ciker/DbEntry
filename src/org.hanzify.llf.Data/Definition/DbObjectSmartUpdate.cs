
#region usings

using System;
using System.Collections.Generic;
using org.hanzify.llf.Data.Common;
using org.hanzify.llf.Data.QuerySyntax;

#endregion

namespace org.hanzify.llf.Data.Definition
{
    [Serializable]
    public class DbObjectSmartUpdate : DbObjectBase
    {
        [Exclude]
        internal protected Dictionary<string, object> m_UpdateColumns = null;

        [Exclude]
        internal bool m_InternalInit = false;

        protected void m_InitUpdateColumns()
        {
            m_UpdateColumns = new Dictionary<string, object>();
        }

        protected internal void m_ColumnUpdated(string ColumnName)
        {
            if (m_UpdateColumns != null && !m_InternalInit)
            {
                m_UpdateColumns[ColumnName] = 1;
            }
        }
    }
}
