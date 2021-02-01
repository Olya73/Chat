using AutoMapper;
using Contract.Bot;
using Contract.DTO;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Mapper
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<MessageAddDTO, Message>();
            CreateMap<MessageAddDTO, MessageGetDTO>();
            CreateMap<Message, MessageGetDTO>().ReverseMap();
            CreateMap<User, UserGetDTO>();
            CreateMap<DialogAddDTO, Dialog>()
                .ForMember(map => map.UserDialogs, map => map.MapFrom(i => i.UserIds));
            CreateMap<int, UserDialog>()
                .ForMember(map => map.UserId, map => map.MapFrom(c => c));
            CreateMap<DialogAddDTO, DialogGetDTO>();
            CreateMap<BotDialogAddDTO, BotDialog>();
            CreateMap<ChatEventDTO, ChatEvent>()
                .ForMember(map => map.TypeOfActionId, map => map.MapFrom(a => (int) Enum.Parse(typeof(ActionTypes), a.TypeOfAction)));
        }
    }
}
