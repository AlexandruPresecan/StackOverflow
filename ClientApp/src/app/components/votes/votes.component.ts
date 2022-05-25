import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Vote } from '../../../models/vote.model';
import { BaseService } from '../../../services/base-service.service';

@Component({
  selector: 'app-votes',
  templateUrl: './votes.component.html',
  styleUrls: ['./votes.component.css'],
  providers: [BaseService]
})

export class VotesComponent<T extends Vote> {

  @Input() voteCount!: number;
  @Input() elementId!: number;
  @Input() votes: T[] | undefined;
  @Output() onVote = new EventEmitter();

  upVoteObject!: T;
  downVoteObject!: T;

  constructor(private router: Router, private voteService: BaseService<number, T>) {

  }

  alreadyVoted(): T | undefined {
    return this.votes?.find(v => v.authorId == localStorage["id"]);
  }

  wasUpVote(): boolean {
    return this.alreadyVoted()?.value == 0;
  }

  wasDownVote(): boolean {
    return this.alreadyVoted()?.value == 1;
  }

  upVote(): void {

    if (!localStorage["token"]) {
      this.router.navigate(['login']);
      return;
    }

    const previousVote = this.alreadyVoted();

    if (!previousVote) {

      this.voteService.create(this.upVoteObject).subscribe(result => {
        this.onVote.emit();
      }, error => console.log(error));

      return;
    }

    if (previousVote.value == 1) {

      this.voteService.update(previousVote.id, this.upVoteObject).subscribe(result => {
        this.onVote.emit();
      }, error => console.log(error));

      return;
    }

    this.voteService.delete(previousVote.id).subscribe(result => {
      this.onVote.emit();
    }, error => console.log(error));
  }

  downVote(): void {

    if (!localStorage["token"]) {
      this.router.navigate(['login']);
      return;
    }

    const previousVote = this.alreadyVoted();

    if (!previousVote) {

      this.voteService.create(this.downVoteObject).subscribe(result => {
        this.onVote.emit();
      }, error => console.log(error));

      return;
    }

    if (previousVote.value == 0) {

      this.voteService.update(previousVote.id, this.downVoteObject).subscribe(result => {
        this.onVote.emit();
      }, error => console.log(error));

      return;
    }

    this.voteService.delete(previousVote.id).subscribe(result => {
      this.onVote.emit();
    }, error => console.log(error));
  }
}
