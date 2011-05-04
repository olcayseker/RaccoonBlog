﻿using AutoMapper;
using RavenDbBlog.Core;
using RavenDbBlog.Core.Models;
using RavenDbBlog.Infrastructure.AutoMapper.Profiles.Resolvers;
using RavenDbBlog.ViewModels;

namespace RavenDbBlog.Infrastructure.AutoMapper.Profiles
{
    public class PostsAdminViewModelMapperProfile : AbstractProfile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Post, PostSummaryJson>()
                .ForMember(x => x.Id, o => o.MapFrom(m => RavenIdResolver.Resolve(m.Id)))
                .ForMember(x => x.Start, o => o.MapFrom(m => m.PublishAt.ToString("yyyy-MM-ddTHH:mm:ssZ")))
                .ForMember(x => x.Url, o => o.MapFrom(m => UrlHelper.Action("Details", "PostAdmin", new { Id = RavenIdResolver.Resolve(m.Id), Slug = SlugConverter.TitleToSlag(m.Title) })))
                .ForMember(x => x.AllDay, o => o.UseValue(false))
                ;
            
            Mapper.CreateMap<Post, PostInput>()
                .ForMember(x => x.Id, o => o.MapFrom(m => RavenIdResolver.Resolve(m.Id)))
                .ForMember(x => x.Tags, o => o.MapFrom(m => TagsResolver.ResolveTags(m.Tags)))
                ;
            
            Mapper.CreateMap<PostInput, Post>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Author, o => o.Ignore())
                .ForMember(x => x.LegacySlug, o => o.Ignore())
                .ForMember(x => x.ShowPostEvenIfPrivate, o => o.Ignore())
                .ForMember(x => x.SkipAutoReschedule, o => o.Ignore())
                .ForMember(x => x.IsDeleted, o => o.Ignore())
                .ForMember(x => x.CommentsCount, o => o.Ignore())
                .ForMember(x => x.CommentsId, o => o.Ignore())
                .ForMember(x => x.LastEditedBy, o => o.Ignore())
                .ForMember(x => x.LastEditedAt, o => o.Ignore())
                .ForMember(x => x.Tags, o => o.MapFrom(m => TagsResolver.ResolveTagsInput(m.Tags)))
                ;

            Mapper.CreateMap<Post, AdminPostDetailsViewModel.PostDetails>()
                .ForMember(x => x.Id, o => o.MapFrom(m => RavenIdResolver.Resolve(m.Id)))
                .ForMember(x => x.Slug, o => o.MapFrom(m => SlugConverter.TitleToSlag(m.Title)))
                .ForMember(x => x.PublishedAt, o => o.MapFrom(m => m.PublishAt))
                .ForMember(x => x.Key, o => o.MapFrom(m => m.ShowPostEvenIfPrivate))
                ;

            Mapper.CreateMap<PostComments.Comment, AdminPostDetailsViewModel.Comment>()
                .ForMember(x => x.Body, o => o.MapFrom(m => MarkdownResolver.Resolve(m.Body)))
                .ForMember(x => x.EmailHash, o => o.MapFrom(m => EmailHashResolver.Resolve(m.Email)))
                .ForMember(x => x.IsImportant, o => o.MapFrom(m => m.Important))
                ;
            
            Mapper.CreateMap<User, Post.AuthorReference>()
                ;
        }
    }
}