using System;
using System.Linq;
using DataverseModel;
using Microsoft.Xrm.Sdk;

namespace Gov.Cscp.VictimServices.Public.Models.Extensions
{
    public static class CreateRestitutionCaseRequestDtoExtensions
    {
        public static VSd_CreateRestitutionCaseRequest ConvertToDynamicsRequest(
            this CreateRestitutionCaseRequestDto model
        )
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Application == null)
            {
                throw new ArgumentNullException(nameof(model.Application));
            }

            var request = new VSd_CreateRestitutionCaseRequest
            {
                Application = model.Application.ToDynamicsEntity(),
                CourtInfoCollection =
                    model.CourtInfoCollection != null
                        ? CreateEntityCollection(
                            model
                                .CourtInfoCollection.Select(c => c.ToDynamicsEntity())
                                .Cast<Entity>()
                                .ToList(),
                            VSd_ApplicationCourtInformation.EntityLogicalName
                        )
                        : CreateEntityCollection(
                            Enumerable.Empty<Entity>().ToList(),
                            VSd_ApplicationCourtInformation.EntityLogicalName
                        ),
                ProviderCollection =
                    model.ProviderCollection != null
                        ? CreateEntityCollection(
                            model
                                .ProviderCollection.Select(p => p.ToDynamicsEntity())
                                .Cast<Entity>()
                                .ToList(),
                            VSd_Participant.EntityLogicalName
                        )
                        : CreateEntityCollection(
                            Enumerable.Empty<Entity>().ToList(),
                            VSd_Participant.EntityLogicalName
                        ),
                DocumentCollection =
                    model.DocumentCollection != null
                        ? CreateEntityCollection(
                            model
                                .DocumentCollection.Select(d => d.ToDynamicsEntity())
                                .Cast<Entity>()
                                .ToList(),
                            ActivityMimeAttachment.EntityLogicalName
                        )
                        : CreateEntityCollection(
                            Enumerable.Empty<Entity>().ToList(),
                            ActivityMimeAttachment.EntityLogicalName
                        ),
            };

            return request;
        }

        private static VSd_Application ToDynamicsEntity(this ApplicationDto application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            var entity = new VSd_Application
            {
                VSd_ApplicantType = (VSd_ApplicantType)application.Applicanttype,
                VSd_ApplicantsFirstName = application.Applicantsfirstname,
                VSd_ApplicantsMiddleName = application.Applicantsmiddlename,
                VSd_ApplicantsLastName = application.Applicantslastname,
                VSd_OtherFirstName = application.Otherfirstname,
                VSd_OtherLastName = application.Otherlastname,
                VSd_ApplicantsGenderCode = ConvertToEnum<VSd_Gender>(
                    application.Applicantsgendercode
                ),
                VSd_ApplicantsBirthdate = application.Applicantsbirthdate,
                VSd_Indigenous = ConvertToEnum<VSd_Application_VSd_Indigenous>(
                    application.Indigenous
                ),
                VSd_ApplicantsPreferredMethodOfContact =
                    ConvertToEnum<VSd_Application_VSd_ApplicantsPreferredMethodOfContact>(
                        application.Applicantspreferredmethodofcontact
                    ),
                VSd_SmsPreferred = ConvertToEnum<VSd_YesNo>(application.Smspreferred),
                VSd_ApplicantsPrimaryPhoneNumber = application.Applicantsprimaryphonenumber,
                VSd_ApplicantsAlternatePhoneNumber = application.Applicantsalternatephonenumber,
                VSd_ApplicantsEmail = application.Applicantsemail,
                VSd_ApplicantsPrimaryAddressLine1 = application.Applicantsprimaryaddressline1,
                VSd_ApplicantsPrimaryAddressLine2 = application.Applicantsprimaryaddressline2,
                VSd_ApplicantsPrimaryAddressLine3 = application.Applicantsprimaryaddressline3,
                VSd_ApplicantsPrimaryCity = application.Applicantsprimarycity,
                VSd_ApplicantsPrimaryProvince = application.Applicantsprimaryprovince,
                VSd_ApplicantsPrimaryPostalCode = application.Applicantsprimarypostalcode,
                VSd_ApplicantsPrimaryCountry = application.Applicantsprimarycountry,
                VSd_CVAp_OffenderFirstName = application.Offenderfirstname,
                VSd_CVAp_OffenderMiddleName = application.Offendermiddlename,
                VSd_CVAp_OffenderLastName = application.Offenderlastname,
                VSd_VoiceMailOption = ConvertToEnum<VSd_VoiceMailOption>(
                    application.Voicemailoption
                ),
                VSd_ApplicantsSignature = application.Applicantssignature,
                VSd_DeclarationFullName = application.Declarationfullname,
                VSd_SigningOfficerTitle = application.Signingofficertitle,
                VSd_DeclarationDate = application.Declarationdate,
                VSd_ContactTitle = application.Contacttitle,
                VSd_OffenderCustodyLocation = application.Offendercustodylocation,
                VSd_GenderIdentityText = application.Genderidentitytext,
                VSd_PrimaryRaceEthnicity = ConvertToEnum<VSd_RaceEthnicity>(
                    application.Primaryraceethnicity
                ),
                VSd_PrimaryRaceEthnicityText = application.Primaryraceethnicitytext,
                VSd_Pronouns = ConvertToEnum<VSd_Pronouns>(application.Pronouns),
                VSd_PronounText = application.Pronountext,
            };

            return entity;
        }

        private static VSd_ApplicationCourtInformation ToDynamicsEntity(this CourtInfoDto courtInfo)
        {
            if (courtInfo == null)
            {
                throw new ArgumentNullException(nameof(courtInfo));
            }

            var entity = new VSd_ApplicationCourtInformation
            {
                VSd_CourtFileNumber = courtInfo.CourtFileNumber,
                VSd_CourtLocation = courtInfo.CourtLocation,
            };

            return entity;
        }

        private static VSd_Participant ToDynamicsEntity(this ParticipantDto participant)
        {
            if (participant == null)
            {
                throw new ArgumentNullException(nameof(participant));
            }

            var entity = new VSd_Participant
            {
                VSd_FirstName = participant.FirstName,
                VSd_MiddleName = participant.MiddleName,
                VSd_LastName = participant.LastName,
                VSd_PreferredName = participant.PreferredName,
                VSd_CompanyName = participant.CompanyName,
                VSd_Name = participant.Name,
                VSd_AddressLine1 = participant.AddressLine1,
                VSd_AddressLine2 = participant.AddressLine2,
                VSd_AddressLine3 = participant.AddressLine3,
                VSd_City = participant.City,
                VSd_Province = participant.Province,
                VSd_Country = participant.Country,
                VSd_PostalCode = participant.PostalCode,
                VSd_PreferredMethodOfContact =
                    ConvertToEnum<VSd_Participant_VSd_PreferredMethodOfContact>(
                        participant.PreferredMethodOfContact
                    ),
                VSd_RestContactPreferenceForUpdates = ConvertToEnum<VSd_Rest_RestContactMethod>(
                    participant.RestContactPreferenceForUpdates
                ),
                VSd_PhoneNumber = participant.PhoneNumber,
                VSd_AlternatePhoneNumber = participant.AlternatePhoneNumber,
                VSd_VoiceMailOptions = ConvertToEnum<VSd_Participant_VSd_VoiceMailOptions>(
                    participant.VoicemailOptions
                ),
                VSd_Email = participant.Email,
                VSd_Rest_CustodyLocation = participant.CustodyLocation,
                VSd_Rest_ProgramName = participant.ProgramName,
                VSd_Relationship1 = participant.Relationship1,
                VSd_Relationship2 = participant.Relationship2,
                VSd_Relationship2Other = participant.Relationship2Other,
                VSd_Title = participant.Title,
                VSd_ContactTitle = participant.ContactTitle,
                VSd_SmsPreferred = ConvertToEnum<VSd_YesNo>(participant.SmsPreferred),
                VSd_IsPrimaryEntityContact = ConvertToEnum<VSd_YesNoUnknown>(
                    participant.IsPrimaryEntityContact
                ),
            };

            return entity;
        }

        private static ActivityMimeAttachment ToDynamicsEntity(this DocumentDto document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var entity = new ActivityMimeAttachment
            {
                FileName = document.Filename,
                Body = document.Body,
                Subject = document.Subject,
            };

            return entity;
        }

        private static TEnum? ConvertToEnum<TEnum>(int? value)
            where TEnum : struct, Enum
        {
            if (!value.HasValue)
            {
                return null;
            }

            if (!Enum.IsDefined(typeof(TEnum), value.Value))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    value.Value,
                    $"Value '{value.Value}' is not valid for enum '{typeof(TEnum).Name}'."
                );
            }

            return (TEnum)(object)value.Value;
        }

        private static EntityCollection CreateEntityCollection(
            System.Collections.Generic.IList<Entity> entities,
            string entityLogicalName
        )
        {
            return new EntityCollection(entities) { EntityName = entityLogicalName };
        }
    }
}
