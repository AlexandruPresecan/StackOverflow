import { User } from "./user.model";

export interface Answer {
  id: number;
  text: string;
  creationDate: Date;
  author?: User;
  voteCount: number;
  questionId: number;
}
