// Mirrors the C# records/DTOs in ABPS_WEB.Domain — kept in one file since the whole
// point is a 1:1 shape match with the backend, not independent client modeling.

export interface LoginRequest { username: string; password: string }
export interface LoginResponse { token: string; username: string; roleId: number; roleName: string }

export interface RoomVM {
  roomId: number;
  roomCategory: string;
  rtId: number;
  roomNo: string;
  rtPrice: number | null;
  roomAdvance: number | null;
  floorId: number;
  floorDescription: string | null;
  beds: number;
  allowed: number;
  isAvailable: boolean;
}

export interface BookingCategory { catId: number; category: string }

export interface BookingRequest {
  name: string;
  address?: string | null;
  aadhaarNo: string;
  phoneNo: string;
  noOfPeople: number;
  catID: number;
  bookCategory: string;
  details?: string | null;
  dtid: number;
  donarID: number;
  bondNo?: string | null;
  noOfRooms: number;
  advance: number;
  totalAmount: number;
  groupBooking?: string | null;
  advanceBooking?: string | null;
  noOfDays: number;
  totalAllowed: number;
  checkinDate: string;
  paymentTypeID: number;
  paymentSubTypeID: number;
  paymentType?: string | null;
  paymentSubType?: string | null;
  trasactionNo?: string | null;
  rooms: RoomVM[];
}

export interface RoomCounts { available: number; occupied: number }

export interface DonarBondVM {
  donarId: number; name: string | null; teluguName: string | null; phoneNo: string | null; aadhaar: string | null;
  address: string | null; bondNo: string | null; roomDays: number; donationAmount: number; donationDate: string;
  donationType: string | null; usedDays: number;
}

export interface CustomerLookupResult { name: string | null; phoneNo: string | null; address: string | null; aadharNo: string | null }

export interface BookingSummary {
  bookingId: number; bookingDate: string; name: string | null; phoneNo: string | null;
  details: string | null; advance: number | null; totalAmount: number | null; checkinDateTime: string | null;
}

export interface BookingRoomLine { roomId: number; roomNo: string | null; roomAmount: number }

export interface OperationResult { success: boolean; message: string }
export interface SaveBookingResult { success: boolean; message: string; bookingId: number }

export interface VacateRoomRequest {
  bookingId: number; checkoutDateTime: string; adjustedAmount: number; refundAmount: number;
  paymentType: string; paymentSubTypeId: number; transactionNo?: string | null; payAmount: number;
  noOfDays: number; roomDonation: number; otherDonation: number;
}

export interface ExtendRoomRequest {
  bookingId: number; noOfDays: number; extendDays: number; totalExtendDays: number; updateTotalAmount: number;
  totalAdvance: number; addAdvance: number; newCheckout: string; paymentTypeId: number;
  paymentSubTypeId: number; transactionNo?: string | null; oldCheckout: string;
}

export interface ShiftRoomRequest {
  fromRoomId: number; toRoomId: number; addAdvance: number; totalAdvance: number; totalAmount: number;
  bookingId: number; paymentTypeId: number; paymentSubTypeId: number; transactionNo?: string | null; checkout: string;
}

export interface CancelRoomRequest { bookingId: number; selectedRoomIds: number[]; checkout: string }

export interface CancelCompleteRoomRequest {
  bookingId: number; selectedRoomIds: number[]; refund: number; payment: number; checkout: string;
  paymentTypeId: number; paymentSubTypeId: number; transactionNo?: string | null;
}

export interface PartialCheckoutRequest { bookingId: number; selectedRoomIds: number[]; checkout: string }

export interface AddAdditionalRoomRequest {
  bookingId: number; updateTotalAmount: number; totalAdvance: number; addAdvance: number;
  bookingDate: string; paymentTypeId: number; paymentSubTypeId: number; transactionNo?: string | null;
  selectedRoomIds: number[];
}

export interface SaveRoomDetailsRequest {
  roomNo: string; roomTypeId: number; floorId: number; roomStatusId: number; bookingId: number;
  price: number; beds: number; allowed: number; advanceAmount: number;
}
export interface ModifyRoomDetailsRequest extends SaveRoomDetailsRequest { roomId: number }

export interface IdName { id: number; name: string | null }
export interface BedOption { value: number; text: string }
export interface RoomTypeOption { rtId: number; rtDesc: string | null; roomCategory: string }
export interface FloorOption { floorId: number; floorDescription: string | null }
export interface PaymentSubTypeOption { paymentSubTypeId: number; paymentSubType: string | null; paymentTypeId: number }
export interface SmsTemplateOption { smstemplateId: number; smscontent: string | null; occassionId: number | null }
export interface StateOption { stateId: number; countryId: number; stateName: string | null }
export interface DistrictOption { districtId: number; stateId: number; countryId: number; districtName: string | null }
export interface TeluguCalendarOption { id: number; name: string | null; nameEnglish: string | null }

export interface LookupsResponse {
  beds: BedOption[];
  countries: IdName[];
  states: StateOption[];
  districts: DistrictOption[];
  donationTypes: IdName[];
  paymentTypes: IdName[];
  paymentSubTypes: PaymentSubTypeOption[];
  occasions: IdName[];
  bookingCategories: IdName[];
  smsTemplates: SmsTemplateOption[];
  teluguMasams: TeluguCalendarOption[];
  teluguPakshams: TeluguCalendarOption[];
  teluguThidis: TeluguCalendarOption[];
  transactionTypes: IdName[];
  transactionCategories: IdName[];
  bookingStatuses: IdName[];
  roomStatuses: IdName[];
  roomTypes: RoomTypeOption[];
  floors: FloorOption[];
  userRoles: IdName[];
}

// ---- Admin ----
export interface ExecutiveMemberReportVM {
  memberName: string; memberType: string; phone: string; email: string; address: string;
  isActive: string; effectiveFrom: string | null; effectiveTo: string | null;
}
export interface MemberVM {
  memberId: number; memberName: string | null; memberTname: string | null; memberType: string | null;
  phone: string | null; email: string | null; address: string | null; isActive: string | null;
  effectiveFrom: string | null; effectiveTo: string | null;
}
export interface SaveExecutiveMemberRequest {
  memberName: string; memberTname: string; phone: string; email: string; address: string;
  memberType: string; isActive: string; effectiveFrom: string; effectiveTo: string;
}
export interface ModifyExecutiveMemberRequest extends SaveExecutiveMemberRequest { memberId: number }

export interface UserVM { userId: number; userName: string | null; email: string | null; mobileNumber: string | null; roleId: number; isActive: string | null }
export interface SaveUserRequest { userName: string; password: string; roleId: number; isActive: string; email: string; phoneNo: string }
export interface ModifyUserRequest { userId: number; newPassword: string; newRoleId: number; isActive: string; email: string; phoneNo: string }

export interface DonorTypeVM { dtid: number; donartype: string | null; code: string | null; amount: number | null; days: number | null }
export interface SaveDonorTypeRequest { donartype: string; code: string; amount: number; days: number }
export interface ModifyDonorTypeRequest extends SaveDonorTypeRequest { dtid: number }

// ---- Donations ----
export interface DonorDto {
  donarId: number; donarTypeId: number; name: string | null; teluguName: string | null; gothram: string | null;
  phoneNo: string | null; emailId: string | null; pan: string | null; aadhaar: string | null; address: string | null;
  districtId: number; stateId: number; countryId: number;
}
export interface DonationDto {
  donationId: number; donarId: number; donationTypeId: number; donationAmount: number; donationDate: string | null;
  paymentTypeId: number | null; paymentSubTypeId: number | null; transactionNo: string | null; bondNo: string | null; roomDays: number;
}
export interface DonationOccasionDto {
  donationOccationId: number; donarId: number; donationId: number; ocassionId: number | null; ocassionName: string;
  intheNameof: string | null; teluguIntheNameof: string | null; anandaanamDate: string | null;
}
export interface DonorInput {
  donationTypeId: number; name?: string | null; teluguName?: string | null; phoneNo?: string | null; address?: string | null;
  gothram?: string | null; pan?: string | null; aadhaar?: string | null; districtId: number; stateId: number; countryId: number;
}
export interface DonationOccasionInput {
  occasionId: number; others?: string | null; intheNameOf?: string | null; teluguIntheNameOf?: string | null;
  anandanamType: string; annadanamDate: string; tMasamId: number; tPakshamId: number; tThiduluId: number; dateEnglishTelugu?: string | null;
}
export interface DonationAmountInput {
  amount: number; paymentTypeId: number; paymentSubTypeId: number; transactionNo?: string | null; bondNo?: string | null;
  donationDate: string; roomDays: number;
}
export interface SaveDonationRequest {
  donor: DonorInput; occasion: DonationOccasionInput; donation: DonationAmountInput;
  transactionCategory: number; occasionName: string; transactionTypeId: number;
}
export interface SaveHistoricalDonationRequest {
  donor: DonorInput; occasion: DonationOccasionInput; donation: DonationAmountInput; occasionName: string;
}
export interface SaveDonationForExistingDonorRequest {
  existingDonarId: number; donationTypeId: number; occasion: DonationOccasionInput; donation: DonationAmountInput;
  transactionCategory: number; occasionName: string; transactionTypeId: number;
}

// ---- Reports ----
export interface DonationReportVM { [key: string]: unknown }
export interface RoomReportVM {
  roomId: number; roomNo: string; category: string; floor: string; beds: number; allowed: number;
  price: number; status: string; customerName: string | null; phoneNo: string | null;
  bookingDate: string | null; bookingId: number | null;
}
export interface TransactionReportVM { [key: string]: unknown }
export interface OccupancyReportVM { [key: string]: unknown }
export interface ExpiringBookingReportVM {
  bookingId: number; name: string | null; phoneNo: string | null; noOfRooms: number | null;
  totalAmount: number | null; bookingLedgerId: number; noOfDays: number | null;
  checkinDateTime: string | null; checkoutDateTime: string | null; expectedCheckoutDate: string;
}
export interface BookingReportVM { [key: string]: unknown }
export interface AuditTransactionReport { [key: string]: unknown }
export interface UserOption { userId: number; userName: string | null }

// ---- Greetings ----
export interface GreetingRecipient {
  name: string | null; phoneNo: string | null; bondNo: string | null; intheNameof: string | null;
  ocassionName: string | null; ocassionId: number;
}
export interface SendGreetingsRecipient extends GreetingRecipient {}
export interface SendGreetingsRequest {
  smsSendType: string; smsDate: string; tMasamId: number; tPakshamId: number; tThiduluId: number;
  recipients: SendGreetingsRecipient[];
}
export interface SendGreetingsResult { sent: number; attempted: number }

// ---- Advance Bookings ----
export interface AdvanceBookingRequest {
  guestName: string; phoneNo: string; aadharNo?: string | null; address?: string | null;
  noPeople: number; noRoomsRequested: number; roomCategoryRtId?: number | null; catId?: number | null; bondNo?: string | null;
  requestedCheckInDate: string; requestedNights: number; advanceDepositAmount?: number | null;
  paymentTypeId?: number | null; paymentSubTypeId?: number | null; referenceNumber?: string | null; notes?: string | null;
}
export interface AdvanceBookingDto extends AdvanceBookingRequest {
  advanceBookingId: number; statusId: number; assignedBookingId: number | null; cancelledReason: string | null;
}
