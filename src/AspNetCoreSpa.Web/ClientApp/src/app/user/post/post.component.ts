import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {DataService} from "@app/services";
import {FormsService} from "@app/shared";
import {DOCUMENT} from "@angular/common";
import { Location } from '@angular/common';

@Component({
  selector: 'appc-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {
  public posts: Post[];
  constructor(@Inject("BASE_URL") private baseUrl: string,
              private route: ActivatedRoute,
              private _renderer2: Renderer2,
              private _dataService:DataService,
              private formsService: FormsService,
              @Inject(DOCUMENT) private _document: Document) { }

  ngOnInit() {
    this.getPosts();
  }

  private getPosts() {
    var data = this._dataService.getFull<Post[]>(`${this.baseUrl}api/post`);
    let that = this;
    data.subscribe((result) => {
      // console.log("Respone:"+result.body);
      let posts = [];
      result.body.forEach((d, i) => {
        posts.push({
          id: d.id,
          name: d.name,
          description: d.description,
          image: d.image,
          postContent: d.postContent,
          metaDescription: d.metaDescription,
          metaKeyWord: d.metaKeyWord,
          alias: d.alias,
          createdAt: d.createdAt,
        });
      });
      that.posts = posts;
      console.log(that.posts);
    }, error => console.error(error));
  }
  
}
