emotes = {
		["/cop"] = { cmd = '/cop', event = 'playCopEmote' },
		["/sit"] = { cmd = '/sit', event = 'playSitEmote' },
		["/chair"] = { cmd = '/chair', event = 'playChairEmote' },
		["/kneel"] = { cmd = '/kneel', event = 'playKneelEmote' },
		["/medic"] = { cmd = '/medic', event = 'playMedicEmote' },
		["/notepad"] = { cmd = '/notepad', event = 'playNotepadEmote' },
		["/traffic"] = { cmd = '/traffic', event = 'playTrafficEmote' },
		["/photo"] = { cmd = '/photo', event = 'playPhotoEmote' },
		["/clipboard"] = { cmd = '/clipboard', event = 'playClipboardEmote' },
		["/lean"] = { cmd = '/lean', event = 'playLeanEmote' },
		["/smoke"] = { cmd = '/smoke', event = 'playSmokeEmote' },
		["/drink"] = { cmd = '/drink', event = 'playDrinkEmote' },
		["/cancelemote"] = { cmd = '/cancelemote', event = 'playCancelEmote' }
}

AddEventHandler('chatMessage', function(source, name, msg)
	if msg == "/emote" then
		CancelEvent();
		TriggerClientEvent('printEmoteList', source);
	elseif emotes[msg].cmd ~= nil then
		CancelEvent();
		TriggerClientEvent(emotes[msg].event, source);
	end
end)