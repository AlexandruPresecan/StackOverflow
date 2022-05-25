import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { QuestionVote } from '../../../models/question-vote.model';
import { QuestionVotesService } from '../../../services/question-votes.service';
import { VotesComponent } from './votes.component';

@Component({
  selector: 'app-question-votes',
  templateUrl: './votes.component.html',
  styleUrls: ['./votes.component.css'],
  providers: [QuestionVotesService]
})

export class QuestionVotesComponent extends VotesComponent<QuestionVote> {

  constructor(router: Router, service: QuestionVotesService) {
    super(router, service);
  }

  ngOnInit() {

    this.upVoteObject = {
      id: 0,
      value: 0,
      questionId: this.elementId
    };

    this.downVoteObject = {
      id: 0,
      value: 1,
      questionId: this.elementId
    };
  }
}
