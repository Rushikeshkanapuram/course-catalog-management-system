export interface LoginResponse {
  token: string;
  expiresAt: string;
  role: string;
  fullName: string;
}