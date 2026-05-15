using System;
using System.Linq;
using DataverseModel;
using Microsoft.Xrm.Sdk;

namespace Gov.Cscp.VictimServices.Public.Models.Extensions
{
    public static class CreateRestitutionCaseRequestDtoExtensions
    {
        public static VSd_CreateRestitutionCaseRequest ConvertToDynamicsRequest(
            this CreateVictimRestitutionCaseRequestDto model
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

            return ConvertToDynamicsRequestInternal(model.Application.ToDynamicsEntity(), model);
        }

        public static VSd_CreateRestitutionCaseRequest ConvertToDynamicsRequest(
            this CreateVictimEntityRestitutionCaseRequestDto model
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

            return ConvertToDynamicsRequestInternal(model.Application.ToDynamicsEntityForVictimEntity(), model);
        }

        public static VSd_CreateRestitutionCaseRequest ConvertToDynamicsRequest(
            this CreateOffenderRestitutionCaseRequestDto model
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

            return ConvertToDynamicsRequestInternal(model.Application.ToDynamicsEntity(), model);
        }

        private static VSd_CreateRestitutionCaseRequest ConvertToDynamicsRequestInternal(
            VSd_Application application,
            RestitutionCaseRequestDtoBase model
        )
        {
            var request = new VSd_CreateRestitutionCaseRequest
            {
                Application = application,
                CourtInfoCollection =
                    model.CourtInfoCollection != null
                        ? CreateEntityCollection(
                            model.CourtInfoCollection.Select(c => c.ToDynamicsEntity()).Cast<Entity>().ToList(),
                            VSd_ApplicationCourtInformation.EntityLogicalName
                        )
                        : CreateEntityCollection(
                            Enumerable.Empty<Entity>().ToList(),
                            VSd_ApplicationCourtInformation.EntityLogicalName
                        ),
                ProviderCollection =
                    model.ProviderCollection != null
                        ? CreateEntityCollection(
                            model.ProviderCollection.Select(p => p.ToDynamicsEntity()).Cast<Entity>().ToList(),
                            VSd_Participant.EntityLogicalName
                        )
                        : CreateEntityCollection(
                            Enumerable.Empty<Entity>().ToList(),
                            VSd_Participant.EntityLogicalName
                        ),
                DocumentCollection =
                    model.DocumentCollection != null
                        ? CreateEntityCollection(
                            model.DocumentCollection.Select(d => d.ToDynamicsEntity()).Cast<Entity>().ToList(),
                            ActivityMimeAttachment.EntityLogicalName
                        )
                        : CreateEntityCollection(
                            Enumerable.Empty<Entity>().ToList(),
                            ActivityMimeAttachment.EntityLogicalName
                        ),
            };

            return request;
        }

        private static VSd_Application ToDynamicsEntity(this VictimApplicationDto application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            var normalized = new ApplicationDto
            {
                ApplicantType = (int)VSd_ApplicantType.Victim,
                ApplicantsFirstName = application.FirstName,
                ApplicantsMiddleName = application.MiddleName,
                ApplicantsLastName = application.LastName,
                OtherFirstName = application.OtherFirstName,
                OtherLastName = application.OtherLastName,
                ApplicantsGenderCode = application.Gender,
                ApplicantsBirthDate = application.BirthDate,
                Indigenous = application.IndigenousStatus,
                ApplicantsPreferredMethodOfContact = application.PreferredMethodOfContact,
                SmsPreferred = application.SmsPreferred,
                ApplicantsPrimaryPhoneNumber = application.PrimaryPhoneNumber,
                ApplicantsAlternatePhoneNumber = application.AlternatePhoneNumber,
                ApplicantsEmail = application.Email,
                ApplicantsPrimaryAddressLine1 = application.PrimaryAddressLine1,
                ApplicantsPrimaryAddressLine2 = application.PrimaryAddressLine2,
                ApplicantsPrimaryAddressLine3 = application.PrimaryAddressLine3,
                ApplicantsPrimaryCity = application.PrimaryCity,
                ApplicantsPrimaryProvince = application.PrimaryProvince,
                ApplicantsPrimaryPostalCode = application.PrimaryPostalCode,
                ApplicantsPrimaryCountry = application.PrimaryCountry,
                ApplicantsSignature = application.Signature,
                OffenderFirstName = application.OffenderFirstName,
                OffenderMiddleName = application.OffenderMiddleName,
                OffenderLastName = application.OffenderLastName,
                VoicemailOption = application.VoicemailOption,
                DeclarationFullName = application.DeclarationFullName,
                SigningOfficerTitle = application.SigningOfficerTitle,
                DeclarationDate = application.DeclarationDate,
                ContactTitle = application.ContactTitle,
                OffenderCustodyLocation = application.OffenderCustodyLocation,
                GenderIdentityText = application.GenderIdentityText,
                PrimaryRaceEthnicity = application.PrimaryRaceEthnicity,
                PrimaryRaceEthnicityText = application.PrimaryRaceEthnicityText,
                Pronouns = application.Pronouns,
                PronounsText = application.PronounsText,
            };

            return normalized.ToDynamicsEntity();
        }

        private static VSd_Application ToDynamicsEntity(this OffenderApplicationDto application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            var normalized = new ApplicationDto
            {
                ApplicantType = (int)VSd_ApplicantType.Offender,
                ApplicantsFirstName = application.FirstName,
                ApplicantsMiddleName = application.MiddleName,
                ApplicantsLastName = application.LastName,
                OtherFirstName = application.OtherFirstName,
                OtherLastName = application.OtherLastName,
                ApplicantsGenderCode = application.Gender,
                ApplicantsBirthDate = application.BirthDate,
                Indigenous = application.IndigenousStatus,
                ApplicantsPreferredMethodOfContact = application.PreferredMethodOfContact,
                SmsPreferred = application.SmsPreferred,
                ApplicantsPrimaryPhoneNumber = application.PrimaryPhoneNumber,
                ApplicantsAlternatePhoneNumber = application.AlternatePhoneNumber,
                ApplicantsEmail = application.Email,
                ApplicantsPrimaryAddressLine1 = application.PrimaryAddressLine1,
                ApplicantsPrimaryAddressLine2 = application.PrimaryAddressLine2,
                ApplicantsPrimaryAddressLine3 = application.PrimaryAddressLine3,
                ApplicantsPrimaryCity = application.PrimaryCity,
                ApplicantsPrimaryProvince = application.PrimaryProvince,
                ApplicantsPrimaryPostalCode = application.PrimaryPostalCode,
                ApplicantsPrimaryCountry = application.PrimaryCountry,
                ApplicantsSignature = application.Signature,
                OffenderFirstName = application.OffenderFirstName,
                OffenderMiddleName = application.OffenderMiddleName,
                OffenderLastName = application.OffenderLastName,
                VoicemailOption = application.VoicemailOption,
                DeclarationFullName = application.DeclarationFullName,
                SigningOfficerTitle = application.SigningOfficerTitle,
                DeclarationDate = application.DeclarationDate,
                ContactTitle = application.ContactTitle,
                OffenderCustodyLocation = application.OffenderCustodyLocation,
                GenderIdentityText = application.GenderIdentityText,
                PrimaryRaceEthnicity = application.PrimaryRaceEthnicity,
                PrimaryRaceEthnicityText = application.PrimaryRaceEthnicityText,
                Pronouns = application.Pronouns,
                PronounsText = application.PronounsText,
            };

            return normalized.ToDynamicsEntity();
        }

        private static VSd_Application ToDynamicsEntityForVictimEntity(this VictimEntityApplicationDto application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            var normalized = new ApplicationDto
            {
                ApplicantType = (int)VSd_ApplicantType.Victim,
                ApplicantsMiddleName = application.MiddleName,
                ApplicantsLastName = application.EntityName,
                OtherFirstName = application.OtherFirstName,
                OtherLastName = application.OtherLastName,
                ApplicantsGenderCode = application.Gender,
                Indigenous = application.IndigenousStatus,
                ApplicantsPreferredMethodOfContact = application.PreferredMethodOfContact,
                SmsPreferred = application.SmsPreferred,
                ApplicantsPrimaryPhoneNumber = application.PrimaryPhoneNumber,
                ApplicantsAlternatePhoneNumber = application.AlternatePhoneNumber,
                ApplicantsEmail = application.Email,
                ApplicantsPrimaryAddressLine1 = application.PrimaryAddressLine1,
                ApplicantsPrimaryAddressLine2 = application.PrimaryAddressLine2,
                ApplicantsPrimaryAddressLine3 = application.PrimaryAddressLine3,
                ApplicantsPrimaryCity = application.PrimaryCity,
                ApplicantsPrimaryProvince = application.PrimaryProvince,
                ApplicantsPrimaryPostalCode = application.PrimaryPostalCode,
                ApplicantsPrimaryCountry = application.PrimaryCountry,
                ApplicantsSignature = application.Signature,
                OffenderFirstName = application.OffenderFirstName,
                OffenderMiddleName = application.OffenderMiddleName,
                OffenderLastName = application.OffenderLastName,
                VoicemailOption = application.VoicemailOption,
                DeclarationFullName = application.DeclarationFullName,
                SigningOfficerTitle = application.SigningOfficerTitle,
                DeclarationDate = application.DeclarationDate,
                ContactTitle = application.ContactTitle,
                OffenderCustodyLocation = application.OffenderCustodyLocation,
                GenderIdentityText = application.GenderIdentityText,
                PrimaryRaceEthnicity = application.PrimaryRaceEthnicity,
                PrimaryRaceEthnicityText = application.PrimaryRaceEthnicityText,
                Pronouns = application.Pronouns,
                PronounsText = application.PronounsText,
            };

            return normalized.ToDynamicsEntity();
        }

        private static VSd_Application ToDynamicsEntity(this ApplicationDto application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            var entity = new VSd_Application
            {
                VSd_ApplicantType = (VSd_ApplicantType)application.ApplicantType,
                VSd_ApplicantsFirstName = application.ApplicantsFirstName,
                VSd_ApplicantsMiddleName = application.ApplicantsMiddleName,
                VSd_ApplicantsLastName = application.ApplicantsLastName,
                VSd_OtherFirstName = application.OtherFirstName,
                VSd_OtherLastName = application.OtherLastName,
                VSd_ApplicantsGenderCode = ConvertToEnum<VSd_Gender>(application.ApplicantsGenderCode),
                VSd_ApplicantsBirthdate = application.ApplicantsBirthDate,
                VSd_Indigenous = ConvertToEnum<VSd_Application_VSd_Indigenous>(application.Indigenous),
                VSd_ApplicantsPreferredMethodOfContact =
                    ConvertToEnum<VSd_Application_VSd_ApplicantsPreferredMethodOfContact>(
                        application.ApplicantsPreferredMethodOfContact
                    ),
                VSd_SmsPreferred = ConvertToEnum<VSd_YesNo>(application.SmsPreferred),
                VSd_ApplicantsPrimaryPhoneNumber = application.ApplicantsPrimaryPhoneNumber,
                VSd_ApplicantsAlternatePhoneNumber = application.ApplicantsAlternatePhoneNumber,
                VSd_ApplicantsEmail = application.ApplicantsEmail,
                VSd_ApplicantsPrimaryAddressLine1 = application.ApplicantsPrimaryAddressLine1,
                VSd_ApplicantsPrimaryAddressLine2 = application.ApplicantsPrimaryAddressLine2,
                VSd_ApplicantsPrimaryAddressLine3 = application.ApplicantsPrimaryAddressLine3,
                VSd_ApplicantsPrimaryCity = application.ApplicantsPrimaryCity,
                VSd_ApplicantsPrimaryProvince = application.ApplicantsPrimaryProvince,
                VSd_ApplicantsPrimaryPostalCode = application.ApplicantsPrimaryPostalCode,
                VSd_ApplicantsPrimaryCountry = application.ApplicantsPrimaryCountry,
                VSd_CVAp_OffenderFirstName = application.OffenderFirstName,
                VSd_CVAp_OffenderMiddleName = application.OffenderMiddleName,
                VSd_CVAp_OffenderLastName = application.OffenderLastName,
                VSd_VoiceMailOption = ConvertToEnum<VSd_VoiceMailOption>(application.VoicemailOption),
                VSd_ApplicantsSignature = application.ApplicantsSignature,
                VSd_DeclarationFullName = application.DeclarationFullName,
                VSd_SigningOfficerTitle = application.SigningOfficerTitle,
                VSd_DeclarationDate = application.DeclarationDate,
                VSd_ContactTitle = application.ContactTitle,
                VSd_OffenderCustodyLocation = application.OffenderCustodyLocation,
                VSd_GenderIdentityText = application.GenderIdentityText,
                VSd_PrimaryRaceEthnicity = ConvertToEnum<VSd_RaceEthnicity>(application.PrimaryRaceEthnicity),
                VSd_PrimaryRaceEthnicityText = application.PrimaryRaceEthnicityText,
                VSd_Pronouns = ConvertToEnum<VSd_Pronouns>(application.Pronouns),
                VSd_PronounText = application.PronounsText,
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
                VSd_PreferredMethodOfContact = ConvertToEnum<VSd_Participant_VSd_PreferredMethodOfContact>(
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
                VSd_IsPrimaryEntityContact = ConvertToEnum<VSd_YesNoUnknown>(participant.IsPrimaryEntityContact),
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
