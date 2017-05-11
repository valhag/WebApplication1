<xsl:stylesheet version = '1.0'
xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
<xsl:template match="/">
  <title>Edo de Cuenta</title>
  <style type="text/css">
    td, th {
    border: 1px solid #999;
    padding: 0rem;
    white-space: nowrap;
    line-height:10px !important;
    overflow: hidden;
    }
    tr{
    height:10px;
    }
    .primertabla{
    line-height:10px !important;
    padding: 0rem;
    }
    .titulo{
    line-height:20x !important;
    padding: 1.0rem;
    text-align: center;
    font-weight: bold;
    border: 1px solid blue #999;
    background-color: #ADD8E6;
    font-size: 20px;
    }
    .segundatabla{
    line-height:10px !important;
    padding: 5px;
    }
    .uno{
    background-color: #FFFFFF;
    }
    .dos{
    background-color: #D3DFEE;
    }
  </style>
  <xsl:variable name="MontoTotal" select="//Total"/>
  <xsl:variable name="MontoVencido" select="//Vencido"/>
  <xsl:variable name="RFCCliente" select="//RFCCliente"/>
  <xsl:variable name="PorVencer" select="//PorVencer"/>
    
  <!--<p>Por medio de este medio me permito hacerte llegar tu estado de cuenta en el que se tiene un importe de <xsl:value-of select="//Total"/></p>-->

  <p>
    Que tal muy buen día <xsl:value-of select="//RazonSocial"/>
  </p>
  <p>
    <!--Por este medio me permito hacer llegar su estado de cuenta, en el que en nuestros registros se tiene un importe de <xsl:value-of select="format-number(($MontoTotal), '$#,###,###.00')"/>
    de los cuales, tenemos un importe vencido de <xsl:value-of select="format-number(($MontoVencido), '$#,###,###.00')"/><br /><br />
    A continuacion le presento tus documentos de manera detallada.-->
    <!--Por este medio nos permitimos hacer llegar la relación de Documentos con Saldo Pendiente que se encuentra en nuestros registros, <br />
    por lo que se lo hacemos llegar para su información y programación de pagos.<br /><br />
    Si existiera algún documento que observe y este haya sido ya liquidado, le pediríamos de favor hacernos llegar el comprobante de pago 
    para de inmediato aplicarlo a su Estado de Cuenta-->
    Por este medio nos permitimos hacerle llegar la relación de Documentos con Saldo Pendiente que se encuentran en nuestros registros, <br />
    por lo que se lo hacemos llegar para su información y programación de pagos.

    <br /><br />
  </p>

  <body>
    
    <table border="1" width="100%">
        <tr>
          <td class="titulo">
            Documentos con saldo pendiente
          </td>
        </tr>
    </table>
    <br />
    <br />
    <div style="border: 5px solid blue; margin: auto;">
        <table border="1" width="100%">
          <colgroup>
            <col width="50%" />
            <col width="25%" />
            <col width="25%" />
          </colgroup>
          <tbody>
            <tr>
              <td class="uno">
                <b>
                  <xsl:text>Cliente:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text>
                </b><xsl:value-of select="$RFCCliente"/>
              </td>
                <td class="dos" align="center" colspan="2">Comportamiento</td>
            </tr>
            <tr>
              <td class="dos">
                <b>
                  <xsl:text>Razon Social:&#160;&#160;</xsl:text>
                </b>     
                <xsl:value-of select="//RazonSocial"/>
              </td>
              <td class="uno">
                <b>
                  <xsl:text> &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Saldo:</xsl:text>
                </b>
              </td>
              <td class="dos">
                <xsl:value-of select="format-number(($MontoTotal), '$#,###,###.00')"/>
              </td>
            </tr>
            <tr>
              <td class="uno">
                <b>
                  <xsl:text>Direccion:&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text>
                </b>     
                <xsl:value-of select="//CalleCliente"/>
              </td>
              <td class="dos">
                <b>
                  <xsl:text> &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Saldo Vencido</xsl:text>
                </b>
              </td>
              <td class="dos">
                <xsl:value-of select="format-number(($MontoVencido), '$#,###,###.00')"/>
              </td>
            </tr>
            <tr>
              <td class="dos">
                <b>
                  <xsl:text>Colonia:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text>
                </b>
                <xsl:value-of select="//ColoniaCliente"/>
              </td>
              <td class="uno"><b>
                <xsl:text> &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Saldo x Vencer:</xsl:text></b></td>
              <td>
                <xsl:value-of select="format-number(($PorVencer), '$#,###,###.00')"/>
              </td>
            </tr>
            <tr>
              <td class="uno">
                <b>
                  <xsl:text>Ciudad:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text>
                </b>     
                <xsl:value-of select="//CiudadCliente"/>
              </td>
              <td  class="dos">
                <b>
                  <xsl:text> &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Fecha de Corte</xsl:text>
                </b>
              </td>
              <td class="dos">
                <xsl:value-of  select="//FechaHoy"/>
              </td>
            </tr>
            <tr>
              <td class="dos">
                <b>
                  <xsl:text>Estado:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text>
                </b>     
                <xsl:value-of select="//EstadoCliente"/>
              </td>
            </tr>
            <tr>
              <td class="uno">
                <b>
                  <xsl:text>Telefono:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text>
                </b>
                <xsl:value-of select="//TelefonoCliente"/>
              </td>
            </tr>
          </tbody>
        </table>
    </div>
    <br />
    <br />
    <table border="1" width="100%">
      <tr>
        <td class="titulo">
          Detalle de Documentos
        </td>
      </tr>
    </table>
    <br />
  <!--<p><xsl:value-of select="//Documentos/Documento/Agente"/></p>-->
  <div style="border: 3px solid blue; margin: auto;">
  <table>
    <thead bgcolor="#add8e6">
      <!--<th>Serie</th>-->
      <th class="segundatabla">Folio</th>
      <th class="segundatabla">Fecha Factura</th>
      <th class="segundatabla">Vencimiento</th>
      <th class="segundatabla">Tipo Documento</th>
      <th class="segundatabla">Operaciones</th>
      <th class="segundatabla">Ejecutivo Comercial</th>
      <th class="segundatabla">Total</th>
      <th class="segundatabla">Pendiente de Pago</th>
      <th class="segundatabla">Concepto</th>
      <th class="segundatabla">Referencia</th>
      <th class="segundatabla">Moneda</th>
    </thead>
    <xsl:for-each select="//Documentos/Documento">
      <xsl:variable name="DocumentoTotal" select="Total"/>
      <xsl:variable name="DocumentoPendiente" select="Pendiente"/>
      <xsl:variable name="DocumentoFecha" select="Fecha"/>
      <xsl:variable name="altColor">
        <xsl:choose>
          <xsl:when test="position() mod 2 = 0">#FFFFFF</xsl:when>
          <xsl:otherwise>#D3DFEE</xsl:otherwise>
        </xsl:choose>
      </xsl:variable>
      <tr style="line-height:14px !important;" bgcolor="{$altColor}">
        <!--<td>
          <xsl:value-of select="Serie" />
        </td>-->
        <td class="segundatabla">
          <xsl:value-of select="Folio" />
        </td>

        <td class="segundatabla">
          <xsl:value-of select="Fecha" />
        </td>
        <td class="segundatabla">
          <xsl:value-of select="Vencimiento"/>
        </td>
        <td class="segundatabla">
          <xsl:value-of select="DocumentoModelo"/>
        </td>
        <td class="segundatabla">
          <xsl:value-of select="Concepto"/>
        </td>
        <td class="segundatabla">
          <xsl:value-of select="Agente"/>
        </td>
        <td class="segundatabla">
          <xsl:value-of select="format-number(($DocumentoTotal), '$#,###,###.00')"/>
        </td>
        <td class="segundatabla">
          <xsl:value-of select="format-number(($DocumentoPendiente), '$#,###,###.00')"/>
        </td>
        <td class="segundatabla">
          <xsl:value-of select="Producto"/>
        </td>
        <td class="segundatabla">
          <xsl:value-of select="Observaciones"/>
        </td>
        <td class="segundatabla">
          <xsl:value-of select="Moneda"/>
        </td>
      </tr>
    </xsl:for-each>   
      
  </table>
  </div>
    <br />
  <p>
    <!-->Agradezco que si alguno de los documentos arriba mencionados ya fue liquidado nos apoyes enviando su respectivo comprobante de pago y asi poder nosotros actualizar tu estado de cuenta</p>-->
    Agradecemos que si alguno de los documentos arriba mencionados ya fue liquidado, nos pueda apoyar enviando su comprobante de pago y así poder nosotros actualizar su estado de cuenta.
  </p>
    <br />
    <p>
      Para su mayor comodidad, ponemos a su disposición nuestros datos bancarios y le puedan servir para realizar su depósito o transferencia.
      <br />
      <xsl:text>Banco:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text><xsl:value-of select="//Banco"/>
      <br />
      <xsl:text>Cuenta Bancaria:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text><xsl:value-of select="//Cuenta"/>
      <br />
      <xsl:text>Clabe Interbancaria:&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text><xsl:value-of select="//CLABE"/>
      <br />
      <xsl:text>Razón Social:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text><xsl:value-of select="//RazonSocialBanco"/>
      <br />
      <xsl:text>RFC:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text><xsl:value-of select="//RFCBanco"/>
    <br />
      <xsl:text>Correo electrónico:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text><xsl:value-of select="//correoconfirmacion"/>
    </p>
    
    
    
    
  <p>Atentamente Departamento de Crédito y Cobranza </p>

    <div style="border: 5px solid blue; margin: auto;">
      <table>
      <tr>
        <td class="uno">
          <b>Proveedor:&#160;&#160;&#160;&#160;&#160;</b>     <xsl:value-of select="//NombreEmpresa"/>
        </td>
      </tr>
      <tr>
        <td class="dos">
          <b>Dirección:&#160;&#160;&#160;&#160;&#160;</b>     <xsl:value-of select="//DireccionEmpresa"/>
        </td>
      </tr>
      <tr>
        <td class="tres">
          <b>Teléfono:&#160;&#160;&#160;&#160;&#160;</b>     <xsl:value-of select="//TelefonoEmpresa"/>
        </td>
      </tr>
    </table>
      </div>
  </body>
  

</xsl:template>


  
</xsl:stylesheet>
