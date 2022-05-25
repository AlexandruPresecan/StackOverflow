import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionsService } from '../../../services/questions.service';
import { Question } from '../../../models/question.model';
import { User } from '../../../models/user.model';

@Component({
  selector: 'app-question-page',
  templateUrl: './question-page.component.html',
  styleUrls: ['./question-page.component.css'],
  providers: [QuestionsService]
})

export class QuestionPageComponent {

  @ViewChild('title') title!: ElementRef;
  @ViewChild('tags') tags!: ElementRef;
  @ViewChild('text') text!: ElementRef;

  question!: Question;
  isEditing: boolean = false;

  constructor(private questionsService: QuestionsService, private route: ActivatedRoute, private router: Router) {

  }

  ngOnInit() {
    this.questionsService.getById(this.route.snapshot.params.id).subscribe(result => {
      this.question = result;
    }, error => console.log(error));
  }

  redirectToAuthor(author: User | undefined): void {
    if (author)
      this.router.navigate(['users', author.id]);
  }

  setTags(): void {
    this.question.tags = this.tags.nativeElement.value.split(" ").filter((tag: string) => tag != "");
  }

  canEdit(): boolean {

    if (!localStorage["token"])
      return false;

    return localStorage["id"] == this.question.author?.id || localStorage["admin"].toString() == "true";
  }

  editQuestion(): void {

    this.isEditing = !this.isEditing;

    if (!this.isEditing) {
      this.ngOnInit();
      return;
    }

    this.text.nativeElement.value = this.question.text;
    this.title.nativeElement.value = this.question.title;
    this.tags.nativeElement.value = this.question.tags.join(" ");
  }

  updateQuestion(): void {

    this.isEditing = false;
    this.question.text = this.text.nativeElement.value;
    this.question.title = this.title.nativeElement.value;

    this.questionsService.update(this.question.id, this.question).subscribe(result => {
      this.ngOnInit();
    }, error => console.log(error));
  }

  deleteQuestion(): void {
    this.questionsService.delete(this.question.id).subscribe(result => {
      this.router.navigate(['questions']);
    }, error => console.log(error));
  }
}
