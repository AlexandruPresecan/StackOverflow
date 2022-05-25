import { Vote } from "./vote.model";

export interface AnswerVote extends Vote {
  answerId: number;
}
