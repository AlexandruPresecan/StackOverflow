import { Answer } from "./answer.model";
import { Question } from "./question.model";

export interface User {
  id: string;
  userName: string;
  email?: string;
  score: number;
  banned: boolean;
  questions?: Question[];
  answers?: Answer[];
  password?: string;
  confirmPassword?: string;
  newPassword?: string;
}
