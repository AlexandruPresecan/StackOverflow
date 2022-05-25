import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AnswerVote } from '../../../models/answer-vote.model';
import { AnswerVotesService } from '../../../services/answer-votes.service';
import { VotesComponent } from './votes.component';

@Component({
  selector: 'app-answer-votes',
  templateUrl: './votes.component.html',
  styleUrls: ['./votes.component.css'],
  providers: [AnswerVotesService]
})

export class AnswerVotesComponent extends VotesComponent<AnswerVote> {

  constructor(router: Router, service: AnswerVotesService) {
    super(router, service);
  }

  ngOnInit() {

    this.upVoteObject = {
      id: 0,
      value: 0,
      answerId: this.elementId
    };

    this.downVoteObject = {
      id: 0,
      value: 1,
      answerId: this.elementId
    };
  }
}
