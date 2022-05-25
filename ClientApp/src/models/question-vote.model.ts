import { Vote } from "./vote.model";

export interface QuestionVote extends Vote {
  questionId: number;
}
