namespace HBLibrary.Test;



[TestClass]
public class AdifWriterTests
{
    [TestMethod]
    public void WriteToAdif_WritesOnlyNonEmptyFields_AndRespectsGuid()
    {
        // Arrange
        var qso = new Qso
        {
            Id = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
            Call = "K1ABC",
            Mode = "SSB",
            Freq = 14.250m,
            QsoDate = new DateTime(2024, 6, 6, 14, 30, 0, DateTimeKind.Utc),
            RstSent = "", // intentionally blank
            RstRcvd = "", // intentionally blank
            Details = new List<QsoDetail>
            {
                new QsoDetail { FieldName = "gridsquare", FieldValue = "FN42" },
                new QsoDetail { FieldName = "qsl_rcvd", FieldValue = "Y" },
                new QsoDetail { FieldName = "empty_field", FieldValue = " " }, // should be skipped
            }
        };

        // Act
        var adif = AdifWriter.WriteToAdif(new[] { qso });

        // Assert: Header present
        StringAssert.Contains(adif, "# ADIF with WA1GON GUID extensions");
        StringAssert.Contains(adif, "<ADIF_VER:");
        StringAssert.Contains(adif, "<EOH>");

        // Assert: Required and present fields
        StringAssert.Contains(adif, "<CALL:5>K1ABC");
        StringAssert.Contains(adif, "<MODE:3>SSB");
        StringAssert.Contains(adif, "<FREQ:6>14.250");
        StringAssert.Contains(adif, "<QSO_DATE:8>20240606");
        StringAssert.Contains(adif, "<TIME_ON:6>143000");
        StringAssert.Contains(adif, "<GUID:36>123e4567-e89b-12d3-a456-426614174000");

        // Assert: Optional fields not present when blank
        StringAssert.DoesNotMatch(adif, new System.Text.RegularExpressions.Regex(@"<RST_SENT:"));
        StringAssert.DoesNotMatch(adif, new System.Text.RegularExpressions.Regex(@"<RST_RCVD:"));

        // Assert: Details correctly added
        StringAssert.Contains(adif, "<GRIDSQUARE:4>FN42");
        StringAssert.Contains(adif, "<QSL_RCVD:1>Y");

        // Assert: No empty/whitespace field written
        Assert.IsFalse(adif.ToUpperInvariant().Contains("<EMPTY_FIELD"), "ADIF should not contain <EMPTY_FIELD>");


        // Assert: <EOR> present
        StringAssert.Contains(adif, "<EOR>");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void WriteToAdif_Throws_WhenBandAndFreqMissing()
    {
        // Arrange: Missing both FREQ and BAND
        var qso = new Qso
        {
            Call = "N0FREQ",
            Mode = "FT8",
            QsoDate = DateTime.UtcNow
        };

        // Act
        AdifWriter.WriteToAdif(new[] { qso });
    }
}
