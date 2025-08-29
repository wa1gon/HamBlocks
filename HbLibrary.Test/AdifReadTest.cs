namespace HBLibrary.Test;

[TestClass]
public class AdifReaderTests
{
    private readonly string tempFilePath = Path.GetTempFileName();

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(tempFilePath))
            File.Delete(tempFilePath);
    }

    [TestMethod]
    public void ReadFromFile_ParsesQso_WithGuidAndDetails()
    {
        // Arrange
        var adifContent = @"
            <ADIF_VER:5>3.1.0
            <PROGRAMID:5>TEST1
            <EOH>
            <CALL:5>K1ABC <BAND:3>20M <MODE:3>SSB <RST_SENT:2>59 <RST_RCVD:2>59 
            <QSO_DATE:8>20240606 <TIME_ON:4>1430 <FREQ:6>14.250 
            <GUID:36>123e4567-e89b-12d3-a456-426614174000 <QSL_RCVD:1>Y <GRIDSQUARE:4>FN42 <EOR>
        ";

        File.WriteAllText(tempFilePath, adifContent);

        // Act
        var qsos = AdifReader.ReadFromFile(tempFilePath);

        // Assert
        Assert.AreEqual(1, qsos.Count);
        var qso = qsos.First();

        // Verify core fields
        Assert.AreEqual("K1ABC", qso.Call);
        Assert.AreEqual("20M", qso.Band);
        Assert.AreEqual("SSB", qso.Mode);
        Assert.AreEqual("59", qso.RstSent);
        Assert.AreEqual("59", qso.RstRcvd);
        Assert.AreEqual(14.250m, qso.Freq);

        var expectedInstant = new DateTime(2024, 6, 6, 14, 30, 0, DateTimeKind.Utc);
        Assert.AreEqual(expectedInstant, qso.QsoDate);

        // Verify GUID preserved from input
        Assert.AreEqual("123e4567-e89b-12d3-a456-426614174000", qso.Id.ToString());
        Assert.IsTrue(Guid.TryParse(qso.Id.ToString(), out _));

        // Verify QsoDetails/Details contains non-core fields
        Assert.IsTrue(qso.Details.Any(d =>
            d.FieldName.Equals("QSL_RCVD", StringComparison.OrdinalIgnoreCase) &&
            d.FieldValue == "Y"));

        Assert.IsTrue(qso.Details.Any(d =>
            d.FieldName.Equals("GRIDSQUARE", StringComparison.OrdinalIgnoreCase) &&
            d.FieldValue == "FN42"));
    }
}