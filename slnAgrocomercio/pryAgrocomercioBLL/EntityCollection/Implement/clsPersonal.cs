﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using pryAgrocomercioDAL;
using pryAgrocomercioBLL.EntityCollection.Interfaces;

namespace pryAgrocomercioBLL.EntityCollection
{
    public class clsPersonal : clsAbstractBase<Personal>, IPersonal
    {
        public DataTable GetList()
        {
            var result = this.GetAll().OrderBy(Per => Per.perApellidoPat)
                .Select(Per => new
                {
                    Per.perCod,
                    Per.tpecod,
                    Per.perNombres,
                    Per.perApellidoPat,
                    Per.perApellidoMat,
                    Per.perDireccion,
                    Per.perTelefono

                });

            return ToDataTable<object>(result.AsQueryable());
        }
        public object GetPersonalPorTipo(int pnTpecod)
        {
            return this.Find(Per => Per.tpecod == pnTpecod);
        }
        public Personal GetPersonal(int pnPerCod)
        {
            return this.Find(Per => Per.perCod == pnPerCod).First<Personal>();
        }
        public int MaxpnPerCod()
        {
            if (this.GetAll().Count() > 0)
            {
                return this.GetAll().Max<Personal>(Per => Per.perCod);
            }
            return 0;
        }
        public DataTable GetList(int _cod_not)
        {
            if (_cod_not == 0)
            {
                return ToDataTable(this.Find(Per => 1 == 1)
                .OrderByDescending(Not => Not.perApellidoPat)
                .Select(Per => new
                {
                    Per.perCod,
                    Per.tpecod,
                    Per.perNombres,
                    Per.perApellidoPat,
                    Per.perApellidoMat,
                    Per.perDireccion,
                    Per.perTelefono,
                    nombre = Per.perNombres + " " + Per.perApellidoPat + " " + Per.perApellidoMat
                })
                .AsQueryable());
            }
            else
            {
                return ToDataTable(this.Find(Per => Per.tpecod == _cod_not)
                .OrderByDescending(Not => Not.perApellidoPat)
                .Select(Per => new
                {
                    Per.perCod,
                    Per.tpecod,
                    Per.perNombres,
                    Per.perApellidoPat,
                    Per.perApellidoMat,
                    Per.perDireccion,
                    Per.perTelefono,
                    nombre = Per.perNombres + Per.perApellidoPat + Per.perApellidoMat
                })
                .AsQueryable());
            }
        }
        public void DeletePersonal(int perCod)
        {
            try
            {
                var Personal = this.Find(Per => Per.perCod == perCod).FirstOrDefault();
                this.Delete(Personal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}
