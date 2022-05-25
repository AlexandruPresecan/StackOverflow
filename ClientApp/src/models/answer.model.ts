import { AnswerVote } from "./answer-vote.model";
import { User } from "./user.model";

export interface Answer {
  id: number;
  text: string;
  creationDate: Date;
  authorId?: string;
  author?: User;
  voteCount: number;
  questionId: number;
  votes?: AnswerVote[];
}
