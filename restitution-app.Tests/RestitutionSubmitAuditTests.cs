using Gov.Cscp.VictimServices.Public;
using Microsoft.Extensions.Logging;

namespace Gov.Cscp.VictimServices.Public.Tests
{
    public class RestitutionSubmitAuditTests
    {
        private sealed record LogEntry(
            LogLevel Level,
            string Message,
            IReadOnlyList<KeyValuePair<string, object?>> State
        );

        private sealed class FakeLogger : ILogger
        {
            public List<LogEntry> Entries { get; } = new();

            public IDisposable? BeginScope<TState>(TState state)
                where TState : notnull => null;

            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(
                LogLevel logLevel,
                EventId eventId,
                TState state,
                Exception? exception,
                Func<TState, Exception?, string> formatter
            )
            {
                var values =
                    state as IReadOnlyList<KeyValuePair<string, object?>> ?? new List<KeyValuePair<string, object?>>();

                Entries.Add(new LogEntry(logLevel, formatter(state, exception), values));
            }
        }

        [Theory]
        [InlineData(RestitutionSubmitAudit.VictimFormType)]
        [InlineData(RestitutionSubmitAudit.VictimEntityFormType)]
        [InlineData(RestitutionSubmitAudit.OffenderFormType)]
        public void WriteSuccess_WritesInformationAuditWithNonPiiIdentifiers(string formType)
        {
            var logger = new FakeLogger();

            RestitutionSubmitAudit.WriteSuccess(logger, formType, "correlation-123");

            var entry = Assert.Single(logger.Entries);
            Assert.Equal(LogLevel.Information, entry.Level);
            Assert.Contains(new KeyValuePair<string, object?>("FormType", formType), entry.State);
            Assert.Contains(new KeyValuePair<string, object?>("CorrelationId", "correlation-123"), entry.State);
            Assert.Contains(new KeyValuePair<string, object?>("Success", true), entry.State);
            Assert.Contains(formType, entry.Message);
            Assert.Contains("correlation-123", entry.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WriteSuccess_WithoutCorrelationId_UsesPlaceholder(string? correlationId)
        {
            var logger = new FakeLogger();

            RestitutionSubmitAudit.WriteSuccess(logger, RestitutionSubmitAudit.VictimFormType, correlationId);

            var entry = Assert.Single(logger.Entries);
            Assert.Contains(new KeyValuePair<string, object?>("CorrelationId", "unknown"), entry.State);
        }

        [Fact]
        public void WriteSuccess_DoesNotLogFormPayloadOrDynamicsResponse()
        {
            var logger = new FakeLogger();

            RestitutionSubmitAudit.WriteSuccess(logger, RestitutionSubmitAudit.VictimFormType, "correlation-123");

            var entry = Assert.Single(logger.Entries);
            var loggedValues = entry
                .State.Where(pair => pair.Key != "{OriginalFormat}")
                .Select(pair => pair.Value?.ToString() ?? string.Empty)
                .ToList();

            // Only form type, correlation id and the success flag may be logged.
            Assert.Equal(3, loggedValues.Count);
            Assert.All(
                loggedValues,
                value =>
                    Assert.Contains(value, new[] { RestitutionSubmitAudit.VictimFormType, "correlation-123", "True" })
            );
            Assert.DoesNotContain("OrganizationResponse", entry.Message);
        }

        [Fact]
        public void WriteDynamicsFailure_LogsIsSuccessAndErrorCodeWithoutResponseBody()
        {
            var logger = new FakeLogger();

            RestitutionSubmitAudit.WriteDynamicsFailure(logger, false, 42, new[] { "IsSuccess", "ErrorCode" });

            var entry = Assert.Single(logger.Entries);
            Assert.Equal(LogLevel.Error, entry.Level);
            Assert.Contains(new KeyValuePair<string, object?>("IsSuccess", false), entry.State);
            Assert.Contains(new KeyValuePair<string, object?>("ErrorCode", 42), entry.State);
            Assert.Contains(new KeyValuePair<string, object?>("ResultKeys", "IsSuccess,ErrorCode"), entry.State);
            Assert.DoesNotContain("{@Response}", entry.Message);
            Assert.DoesNotContain("OrganizationResponse", entry.Message);
        }

        [Fact]
        public void WriteDynamicsFailure_DoesNotAcceptOrganizationResponseParameter()
        {
            // Guard: method signature is scalars/keys only — no OrganizationResponse overload.
            var methods = typeof(RestitutionSubmitAudit)
                .GetMethods()
                .Where(m => m.Name == nameof(RestitutionSubmitAudit.WriteDynamicsFailure));
            Assert.All(
                methods,
                m =>
                    Assert.DoesNotContain(m.GetParameters(), p => p.ParameterType.Name.Contains("OrganizationResponse"))
            );
        }
    }
}
