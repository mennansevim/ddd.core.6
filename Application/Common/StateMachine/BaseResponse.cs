using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.StateMachine.Responses;

namespace Infrastructure.Common.Data;

public class BaseResponse
{
    public bool HasError => this.Messages.Any<MessageDto>((Func<MessageDto, bool>) (m => m.Type == MessageType.Error));

    public List<MessageDto> Messages { get; set; }

    public BaseResponse() => this.Messages = new List<MessageDto>();

    public void AddErrorMessage(string content) => this.AddMessage(content, MessageType.Error);

    public void AddInfoMessage(string content) => this.AddMessage(content, MessageType.Info);

    public void AddSuccessMessage(string content) => this.AddMessage(content, MessageType.Success);

    public void AddWarningMessage(string content) => this.AddMessage(content, MessageType.Warning);

    private void AddMessage(string content, MessageType type) => this.Messages.Add(new MessageDto(content, type));
}