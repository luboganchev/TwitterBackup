﻿namespace TwitterBackup.Web.Models.Tweets
{
    using AutoMapper;
    using System;
    using Tweetinvi.Models;
    using TwitterBackup.Models;
    using TwitterBackup.Web.Helpers.AutoMapper;
    using TwitterBackup.Web.Models.Users;

    public class TweetViewModel : IHaveCustomMappings
    {
        public string IdString { get; set; }

        public UserShortInfoViewModel Owner { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Text { get; set; }

        public string FullText { get; set; }

        public int FavoriteCount { get; set; }

        public int RetweetCount { get; set; }

        public bool RetweetedFromMe { get; set; }

        public bool IsRetweet { get; set; }

        public bool HasStored { get; set; }

        public UserShortInfoViewModel RetweetFrom { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
             configuration.CreateMap<ITweet, TweetViewModel>()
                 .ForMember(dest => dest.IdString, opt => opt.MapFrom(src => src.IdStr))
                 .ForMember(dest => dest.RetweetedFromMe, opt => opt.MapFrom(src => src.Retweeted))
                 .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => Mapper.Map<IUser, UserShortInfoViewModel>(src.CreatedBy)))
                 .ForMember(dest => dest.RetweetFrom, opt => opt.MapFrom(src => src.IsRetweet? Mapper.Map<IUser, UserShortInfoViewModel>(src.RetweetedTweet.CreatedBy): null));

            configuration.CreateMap<TweetViewModel, Tweet>()
                .ForMember(dest => dest.TweetTwitterId, opt => opt.MapFrom(src => long.Parse(src.IdString)));
        }
    }
}