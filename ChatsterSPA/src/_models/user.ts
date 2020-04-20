import { Photo } from './photo';

export interface User {
  id: number;
  username: string;
  gender: string;
  age: number;
  knownAs: string;
  introduction?: string;
  createdDate?: Date;
  lastActive?: Date;
  interests?: string;
  lookingFor?: string;
  city?: string;
  country?: string;
  photoUrl?: string;
  photos?: Photo[];
}
