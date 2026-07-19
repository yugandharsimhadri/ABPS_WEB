import { http } from './http';
import type * as T from './types';

export const authApi = {
  login: (req: T.LoginRequest) => http.post<T.LoginResponse>('/auth/login', req),
};

export const lookupsApi = {
  getAll: () => http.get<T.LookupsResponse>('/lookups'),
};

export const roomsApi = {
  getAvailable: () => http.get<T.RoomVM[]>('/rooms/available'),
  getAvailableDormLocker: () => http.get<T.RoomVM[]>('/rooms/available/dorm-locker'),
  getCounts: () => http.get<T.RoomCounts>('/rooms/counts'),
  getDormLockerCounts: () => http.get<T.RoomCounts>('/rooms/counts/dorm-locker'),
  getDonarByBondNo: (bondNo: string) => http.get<T.DonarBondVM>(`/rooms/donors/by-bond-no/${encodeURIComponent(bondNo)}`),
  getCustomerByAadhar: (aadhaar: string) => http.get<T.CustomerLookupResult>(`/rooms/guests/by-aadhaar/${encodeURIComponent(aadhaar)}`),
  getCategories: () => http.get<T.BookingCategory[]>('/rooms/categories'),
  getBookingIdByRoomNo: (roomNo: string) => http.get<number>(`/rooms/by-room-no/${encodeURIComponent(roomNo)}`),
  isBookingActive: (bookingId: number) => http.get<boolean>(`/rooms/bookings/${bookingId}/is-active`),
  getBookingDetails: (bookingId: number) => http.get<T.BookingSummary>(`/rooms/bookings/${bookingId}`),
  getBookingRooms: (bookingId: number) => http.get<T.BookingRoomLine[]>(`/rooms/bookings/${bookingId}/rooms`),
  saveRoomDetails: (req: T.SaveRoomDetailsRequest) => http.post<{ message: string }>('/rooms/master', req),
  modifyRoomDetails: (roomId: number, req: T.ModifyRoomDetailsRequest) => http.put<{ message: string }>(`/rooms/master/${roomId}`, req),
  book: (req: T.BookingRequest) => http.post<{ bookingId: number; message: string }>('/rooms/bookings', req),
  vacate: (bookingId: number, req: T.VacateRoomRequest) => http.post<{ message: string }>(`/rooms/bookings/${bookingId}/vacate`, req),
  extend: (bookingId: number, req: T.ExtendRoomRequest) => http.post<{ message: string }>(`/rooms/bookings/${bookingId}/extend`, req),
  shift: (bookingId: number, req: T.ShiftRoomRequest) => http.post<{ message: string }>(`/rooms/bookings/${bookingId}/shift`, req),
  cancel: (bookingId: number, req: T.CancelRoomRequest) => http.post<{ message: string }>(`/rooms/bookings/${bookingId}/cancel`, req),
  cancelComplete: (bookingId: number, req: T.CancelCompleteRoomRequest) => http.post<{ message: string }>(`/rooms/bookings/${bookingId}/cancel-complete`, req),
  partialCheckout: (bookingId: number, req: T.PartialCheckoutRequest) => http.post<{ message: string }>(`/rooms/bookings/${bookingId}/partial-checkout`, req),
  addRoom: (bookingId: number, req: T.AddAdditionalRoomRequest) => http.post<{ message: string }>(`/rooms/bookings/${bookingId}/add-room`, req),
  uploadDocument: (bookingId: number, documentType: string, file: File) => {
    const form = new FormData();
    form.append('file', file);
    form.append('documentType', documentType);
    return http.postForm<void>(`/rooms/bookings/${bookingId}/documents`, form);
  },
};

export const adminApi = {
  getMembers: (fromDate: string, toDate: string, statusId?: string, selectedIndex?: number) =>
    http.get<T.ExecutiveMemberReportVM[]>('/admin/members', { fromDate, toDate, statusId, selectedIndex }),
  getAllMembers: () => http.get<T.MemberVM[]>('/admin/members/all'),
  saveMember: (req: T.SaveExecutiveMemberRequest) => http.post<{ message: string }>('/admin/members', req),
  modifyMember: (memberId: number, req: T.ModifyExecutiveMemberRequest) => http.put<{ message: string }>(`/admin/members/${memberId}`, req),

  getUsers: () => http.get<T.UserVM[]>('/admin/users'),
  getUser: (userId: number) => http.get<T.UserVM>(`/admin/users/${userId}`),
  userExists: (username: string) => http.get<boolean>('/admin/users/exists', { username }),
  addUser: (req: T.SaveUserRequest) => http.post<{ message: string }>('/admin/users', req),
  modifyUser: (userId: number, req: T.ModifyUserRequest) => http.put<{ message: string }>(`/admin/users/${userId}`, req),

  validateAdminCredentials: (username: string, password: string) =>
    http.post<{ message: string }>('/admin/validate', { username, password }),

  getDonorTypes: () => http.get<T.DonorTypeVM[]>('/admin/donor-types'),
  saveDonorType: (req: T.SaveDonorTypeRequest) => http.post<{ message: string }>('/admin/donor-types', req),
  modifyDonorType: (dtid: number, req: T.ModifyDonorTypeRequest) => http.put<{ message: string }>(`/admin/donor-types/${dtid}`, req),
};

export const donationsApi = {
  getDonorById: (donarId: number) => http.get<T.DonorDto[]>(`/donations/donors/${donarId}`),
  getByBondNo: (bondNo: string) => http.get<T.DonationDto[]>(`/donations/by-bond-no/${encodeURIComponent(bondNo)}`),
  getOccasionsByBondNo: (bondNo: string) => http.get<T.DonationOccasionDto[]>(`/donations/occasions/by-bond-no/${encodeURIComponent(bondNo)}`),
  searchDonors: (term: string, type: string) => http.get<T.DonorDto[]>('/donations/donors/search', { term, type }),
  bondNoExists: (bondNo: string) => http.get<boolean>(`/donations/bond-no/${encodeURIComponent(bondNo)}/exists`),
  save: (req: T.SaveDonationRequest) => http.post<{ message: string }>('/donations', req),
  saveHistorical: (req: T.SaveHistoricalDonationRequest) => http.post<{ message: string }>('/donations/historical', req),
  saveForExistingDonor: (req: T.SaveDonationForExistingDonorRequest) => http.post<{ message: string }>('/donations/existing-donor', req),
};

export const reportsApi = {
  getDonationReport: (from: string, to: string) => http.get<T.DonationReportVM[]>('/reports/donations', { from, to }),
  getBookingReport: (from: string, to: string, statusId: number) => http.get<T.BookingReportVM[]>('/reports/bookings', { from, to, statusId }),
  getRoomReport: (rtId: number, floorId: number, statusId: number, beds: number) =>
    http.get<T.RoomReportVM[]>('/reports/rooms', { rtId, floorId, statusId, beds }),
  getTransactionReport: (from: string, to: string, categoryId: number, typeId: number) =>
    http.get<T.TransactionReportVM[]>('/reports/transactions', { from, to, categoryId, typeId }),
  getExpiringBookingReport: () => http.get<T.ExpiringBookingReportVM[]>('/reports/expiring-bookings'),
  getOccupancyReport: () => http.get<T.OccupancyReportVM[]>('/reports/occupancy'),
  getAuditTransactionReport: (from: string, to: string) => http.get<T.AuditTransactionReport[]>('/reports/audit', { from, to }),
  getAllUsers: () => http.get<T.UserOption[]>('/reports/users'),
};

export const greetingsApi = {
  getSentTodayCount: () => http.get<number>('/greetings/sent-today-count'),
  findByEnglishDate: (date: string) => http.get<T.GreetingRecipient[]>('/greetings/recipients/by-date', { date }),
  findByTeluguCalendar: (tMasamId: number, tPakshamId: number, tThiduluId: number) =>
    http.get<T.GreetingRecipient[]>('/greetings/recipients/by-telugu-calendar', { tMasamId, tPakshamId, tThiduluId }),
  send: (req: T.SendGreetingsRequest) => http.post<T.SendGreetingsResult>('/greetings/send', req),
};

export const advanceBookingsApi = {
  create: (req: T.AdvanceBookingRequest) => http.post<{ advanceBookingId: number; message: string }>('/advance-bookings', req),
  search: (params: { phone?: string; name?: string; date?: string; statusId?: number; aadhaar?: string }) =>
    http.get<T.AdvanceBookingDto[]>('/advance-bookings', params),
  getDue: (asOf: string) => http.get<T.AdvanceBookingDto[]>('/advance-bookings/due', { asOf }),
  assign: (advanceBookingId: number, resultingBookingId: number) =>
    http.post<{ message: string }>(`/advance-bookings/${advanceBookingId}/assign/${resultingBookingId}`),
  cancel: (advanceBookingId: number, reason: string) =>
    http.post<{ message: string }>(`/advance-bookings/${advanceBookingId}/cancel`, reason),
};
