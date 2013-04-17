<?xml version="1.0"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="SalaryHistory">
  <html>
  <body>
    <h2>Salary History of <xsl:value-of select="UserName"/></h2>
    <table border="1">
      <tr bgcolor="#9acd32">
        <th>Date</th>
        <th>Amount</th>
      </tr>
      <xsl:for-each select="History/Record">
        <tr>
          <td><xsl:value-of select="Date"/></td>
          <td><xsl:value-of select="Amount"/></td>
        </tr>
      </xsl:for-each>
    </table>
  </body>
  </html>
</xsl:template>

</xsl:stylesheet>