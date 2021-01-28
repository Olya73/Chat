using AutoMapper;
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
            CreateMap<Message, MessageGetDTO>();
            CreateMap<User, UserGetDTO>();
            CreateMap<DialogAddDTO, Dialog>()
                .ForMember(map => map.UserDialogs, map => map.MapFrom(i => i.UserIds));
            CreateMap<int, UserDialog>()
                .ForMember(map => map.UserId, map => map.MapFrom(c => c));
            CreateMap<DialogAddDTO, DialogGetDTO>();
            CreateMap<BotDialogAddDTO, BotDialog>();
        }
    }
}
