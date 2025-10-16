export const MY_FORMATS = {
  parse: {
    dateInput: 'LL'
  },
  display: {
    dateInput: 'YYYY-MM-DD',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'YYYY-MM-DD',
    monthYearA11yLabel: 'MMMM YYYY'
  }
};

export class EnumHelper {
  public GenderOptions = {
    0: '--',
    100000000: 'M',
    100000001: 'F',
    100000002: 'X'
  };

  public RelationshipToVictim = {
    0: '--',
    100000000: 'Spouse',
    100000001: 'Parent/Guardian',
    100000002: 'Child',
    100000003: 'Sibling',
    100000004: 'Other'
  };

  public MaritalStatus = {
    0: '--',
    100000000: 'Married',
    100000001: 'Common Law',
    100000002: 'Widowed',
    100000003: 'Divorced',
    100000004: 'Separated',
    100000005: 'Single'
  };

  public VoicemailOptions = {
    0: '',
    100000000: 'Primary and Alternate',
    100000001: 'Primary only',
    100000002: 'Alternate only',
    100000003: 'No Voicemail'
  };

  public ApplicantPreferredMethodOfContact = {
    0: '--',
    2: 'Phone Call',
    1: 'Email',
    4: 'Primary Mail',
    100000002: 'Alternate Mail'
  };

  public ParticipantPreferredMethodOfContact = {
    // There are two drop downs being accessed by this which is why items appear twice
    0: '--',
    100000000: 'Email',
    100000001: 'Phone Call',
    100000002: 'Mail'
  };

  public ReportMadeToPolice = {
    0: '--',
    100000000: 'Yes',
    100000001: 'No',
    100000002: 'Unknown'
  };

  public OffenderBeenCharged = {
    0: '--',
    100000000: 'Yes',
    100000001: 'No',
    100000002: 'Unknown'
  };

  public HaveYouSuedOffender = {
    0: '--',
    100000000: 'No',
    100000001: 'Yes'
  };

  public IntendToSueOffender = {
    0: '--',
    100000000: 'Yes',
    100000001: 'No',
    100000002: 'Undecided'
  };

  public ApplyToCourtForMoneyFromOffender = {
    0: '--',
    100000000: 'Yes',
    100000001: 'No',
    100000002: 'Not Applicable (No trial/conviction)'
  };

  public WillBeTakingLegalAction = {
    0: '--',
    100000000: 'Yes',
    100000001: 'No',
    100000002: 'Unsure'
  };

  public WereYouEmployedAtTimeOfCrime = {
    0: '--',
    100000000: 'No',
    100000001: 'Yes'
  };

  public AllowCvapStaffSharing = {
    0: '--',
    100000000: 'No',
    100000001: 'Yes-Person',
    100000002: 'Yes-Agency'
  };

  public AllowCvapStaffSharingValues = {
    No: 100000000,
    Yes_Person: 100000001,
    Yes_Agency: 100000002
  };

  public boolEnum = {
    0: '--',
    100000000: 'No',
    100000001: 'Yes'
  };

  public boolValues = {
    No: 100000000,
    Yes: 100000001
  };

  public CompletingOnBehalfOf = {
    0: '--',
    100000000: 'Completing this application for myself',
    100000001: 'A Victim Service Worker or other person helping a victim complete this application',
    100000002: 'A parent completing this application for my minor child (under 19 years of age)',
    100000003: 'A legal representative or legal guardian completing this application on behalf of someone else'
  };

  public ProviderSpecialistType = {
    0: '--',
    100000001: 'Specialist',
    100000002: 'Counsellor / Psychologist',
    100000003: 'Dentist',
    100000004: 'Other'
  };

  public InvoiceCounsellingType = {
    0: '--',
    100000000: 'Counselling Session',
    100000001: 'Court Supporting Counselling',
    100000002: 'Psycho-educational sessions'
  };

  public CRMGender = {
    Male: <IOptionSetVal>{ val: 100000000, name: 'Man/Boy (M)' },
    Female: <IOptionSetVal>{ val: 100000001, name: 'Woman/Girl (F)' },
    NonBinary: <IOptionSetVal>{ val: 100000002, name: 'Non-Binary (X)' },
    PreferNotToAnswer: <IOptionSetVal>{ val: 100000003, name: 'Prefer not to answer (U)' },
    SelfDescribe: <IOptionSetVal>{ val: 100000004, name: 'Prefer to self-describe' }
  };

  public CRMPronoun = {
    SheHerHers: <IOptionSetVal>{ val: 100000001, name: 'She/Her/Hers' },
    HeHimHis: <IOptionSetVal>{ val: 100000000, name: 'He/Him/His' },
    TheyThemTheirs: <IOptionSetVal>{ val: 100000002, name: 'They/Them/Theirs' },
    Other: <IOptionSetVal>{ val: 100000003, name: 'Other' },
  }

  public CRMRaceEthnicity: { [key: string]: IOptionSetVal } = {
    Black: <IOptionSetVal>{ val: 100000000, name: 'Black' },
    Chinese: <IOptionSetVal>{ val: 100000001, name: 'Chinese' },
    Japanese: <IOptionSetVal>{ val: 100000002, name: 'Japanese' },
    Korean: <IOptionSetVal>{ val: 100000003, name: 'Korean' },
    Filipino: <IOptionSetVal>{ val: 100000004, name: 'Filipino' },
    SoutheastAsian: <IOptionSetVal>{ val: 100000005, name: 'Southeast Asian' },
    LatinAmerican: <IOptionSetVal>{ val: 100000006, name: 'Latin American' },
    Arab: <IOptionSetVal>{ val: 100000007, name: 'Arab' },
    WestAsian: <IOptionSetVal>{ val: 100000008, name: 'West Asian' },
    SouthAsian: <IOptionSetVal>{ val: 100000009, name: 'South Asian' },
    White: <IOptionSetVal>{ val: 100000009, name: 'White' },
    Other: <IOptionSetVal>{ val: 100000011, name: 'Other' },
    Multiple: <IOptionSetVal>{ val: 100000012, name: 'Multiple' },
    Indigenous: <IOptionSetVal>{ val: 100000013, name: 'Indigenous' },
  };

  public ContactMethods = {
    BLANK: <IOptionSetVal>{ val: 0, name: '--' },
    Phone: <IOptionSetVal>{ val: 2, name: 'Phone Call' },
    Email: <IOptionSetVal>{ val: 1, name: 'Email' },
    Mail: <IOptionSetVal>{ val: 4, name: 'Mail' }
  };

  public ParticipantContactMethods = {
    Email: <IOptionSetVal>{ val: 100000000, name: 'Email' },
    Phone: <IOptionSetVal>{ val: 100000001, name: 'Phone Call' },
    Mail: <IOptionSetVal>{ val: 100000002, name: 'Mail' }
  };

  public ParticipantRestitutionContactMethods = {
    BLANK: <IOptionSetVal>{ val: 0, name: '--' },
    Email: <IOptionSetVal>{ val: 100000000, name: 'Email' },
    Mail: <IOptionSetVal>{ val: 100000001, name: 'Mail' },
    Phone: <IOptionSetVal>{ val: 100000002, name: 'Phone Call' },
    SMS: <IOptionSetVal>{ val: 100000003, name: 'SMS' }
  };

  public IndigenousStatus = {
    BLANK: <IOptionSetVal>{ val: 0, name: '--' },
    First_Nations: <IOptionSetVal>{ val: 100000000, name: 'First Nations' },
    Metis: <IOptionSetVal>{ val: 100000001, name: 'Métis' },
    Inuit: <IOptionSetVal>{ val: 100000002, name: 'Inuit' },
    Prefere_Not_To_Answer: <IOptionSetVal>{ val: 100000003, name: 'Prefer Not to Answer' },
    Not_Applicable: <IOptionSetVal>{ val: 100000004, name: 'Not Applicable' }
  };

  public LeaveVoicemail = {
    BLANK: <IOptionSetVal>{ val: null, name: '--' },
    Primary_And_Alternate: <IOptionSetVal>{ val: 100000000, name: 'Primary and Alternate' },
    Primary_Only: <IOptionSetVal>{ val: 100000001, name: 'Primary only' },
    Altrernate_Only: <IOptionSetVal>{ val: 100000002, name: 'Alternate only' },
    No_Voicemail: <IOptionSetVal>{ val: 100000003, name: 'No Voicemail' }
  };
}

export const ResitutionForm = {
  Victim: <IOptionSetVal>{ val: 100000002, name: 'Victim' },
  Offender: <IOptionSetVal>{ val: 100000003, name: 'Accused/Offender' },
  VictimEntity: <IOptionSetVal>{ val: 100000004, name: 'Entity Victim' }
};

export enum CRMBoolean {
  True = 100000001,
  False = 100000000
}

export enum CRMMultiBoolean {
  True = 100000000,
  False = 100000001,
  Undecided = 100000002
}

export enum ApplicationType {
  Victim_Application = 100000002,
  IFM_Application = 100000001,
  Witness_Application = 100000000,
  Offender_Application = 100000003
}

export enum OnBehalfOf {
  Myself = 100000000,
  Parent = 100000002,
  Legal_Guardian = 100000003
}

export interface IOptionSetVal {
  name: string;
  val: number;
}
