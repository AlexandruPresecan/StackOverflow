import { Answer } from "./answer.model";
import { Question } from "./question.model";

export interface User {
  id: string;
  userName: string;
  score: number;
  questions?: Question[];
  answers?: Answer[];
}
