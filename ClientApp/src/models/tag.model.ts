import { Question } from "./question.model";

export interface Tag {
  id: number;
  name: string;
  questions: Question[];
}
