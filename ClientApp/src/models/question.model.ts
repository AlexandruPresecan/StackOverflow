import { Answer } from "./answer.model";
import { QuestionVote } from "./question-vote.model";
import { User } from "./user.model";

export interface Question {
  id: number;
  title: string;
  text: string;
  authorId?: string;
  author?: User;
  tags: string[];
  voteCount: number;
  creationDate: Date;
  answers?: Answer[];
  votes?: QuestionVote[];
}
