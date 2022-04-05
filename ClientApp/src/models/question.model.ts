import { Answer } from "./answer.model";
import { User } from "./user.model";

export interface Question {
  id: number;
  title: string;
  text: string;
  author?: User;
  tags: string[];
  voteCount: number;
  creationDate: Date;
  answers?: Answer[];
}
